﻿using System.Linq.Expressions;
using System.Reflection;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoExtensions;

public static class AutoComponentTypeExtensions
{
    public static bool IsImage(this BaseAutoComponentTypeEnumeration? componentType)
    {
        if (componentType is null)
        {
            return false;
        }

        return true; /*componentType.Name switch
        {
            nameof(AutoComponentType.ImageCircle)    => true,
            nameof(AutoComponentType.ImageRectangle) => true,
            nameof(AutoComponentType.ImageRounded)   => true,
            _                                        => false
        };*/
    }

    public static bool IsFormInput(this BaseAutoComponentTypeEnumeration? componentType)
    {
        if (componentType is null)
        {
            return false;
        }

        return componentType.GetType() == typeof(AutoComponentInputType);
    }

    public static PropertyInfo GetPropertyInfoForType<TProperty>(this Expression<Func<TProperty>> expression, Type type)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var property = type.GetProperty(propertyName);

        if (property == null)
        {
            throw new InvalidOperationException($"No property {propertyName} found in {type.Name}");
        }

        return property;
    }

    public static AutoComponentInputType GetDefaultComponentForForm(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoComponentInputType.AutoTextComponentInput,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoComponentInputType.AutoNumericComponentInput,
            not null when propType == typeof(DateTime) => AutoComponentInputType.AutoDateTimeComponentInput,
            not null when propType == typeof(bool) => AutoComponentInputType.AutoSwitchComponentInput,
            _ => AutoComponentInputType.AutoTextComponentInput
        };
    }

    public static AutoComponentType GetDefaultComponentForTable(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoComponentType.AutoTextTable,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoComponentType.AutoNumeric,
            not null when propType == typeof(DateTime) => AutoComponentType.AutoDate,
            not null when propType == typeof(bool) => AutoComponentType.AutoStatus,
            _ => AutoComponentType.AutoTextTable
        };
    }

    public static AutoComponentType GetDefaultComponentForDetails(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoComponentType.AutoText,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoComponentType.AutoNumeric,
            not null when propType == typeof(DateTime) => AutoComponentType.AutoDate,
            not null when propType == typeof(bool) => AutoComponentType.AutoStatus,
            _ => AutoComponentType.AutoText
        };
    }
}