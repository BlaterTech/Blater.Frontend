using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Helpers;

namespace Blater.Frontend.Client.Services;

[SuppressMessage("Usage", "CA2211:Campos não constantes não devem ser visíveis")]
public static class StateNotifierService
{
    public static event Action<object?>? StateChanged;
    
    public static void NotifyStateChanged<TProperty>(Expression<Func<TProperty>> expression)
    {
        var value = GetPropertyValueFromExpression(expression);
        StateChanged?.Invoke(value);
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