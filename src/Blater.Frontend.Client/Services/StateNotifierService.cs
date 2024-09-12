﻿using System.Linq.Expressions;
using System.Reflection;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Helpers;

namespace Blater.Frontend.Client.Services;

public static class StateNotifierService
{
    public static event Action<PropertyInfo, object?>? StateChanged;
    
    public static void NotifyStateChanged<TProperty>(Expression<Func<TProperty>> expression)
    {
        var typeName = GetClassNameFromExpression(expression);
        var modelType = typeName.GetTypeFromName();
        var propertyInfo = expression.GetPropertyInfoForType(modelType);
        var value = GetPropertyValueFromExpression(expression);
        StateChanged?.Invoke(propertyInfo, value);
    }
    
    private static string GetClassNameFromExpression<TProperty>(Expression<Func<TProperty>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.DeclaringType?.Name!;
        }
        
        throw new ArgumentException("Invalid Expression");
    }
    
    private static object? GetPropertyValueFromExpression<TProperty>(Expression<Func<TProperty>> expression)
    {
        var func = expression.Compile();
        return func();
    }
}