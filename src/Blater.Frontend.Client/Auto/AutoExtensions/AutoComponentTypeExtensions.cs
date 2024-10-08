﻿using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Enumerations;

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

        return componentType.GetType() == typeof(AutoFormComponentInputType);
    }

    public static AutoFormComponentInputType GetDefaultComponentForForm(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoFormComponentInputType.AutoFormTextComponentInput,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoFormComponentInputType.AutoFormNumericComponentInput,
            not null when propType == typeof(DateTime) || propType == typeof(DateTimeOffset) => AutoFormComponentInputType.AutoFormDateTimeComponentInput,
            not null when propType == typeof(bool) => AutoFormComponentInputType.AutoFormSwitchComponentInput,
            _ => AutoFormComponentInputType.AutoFormTextComponentInput
        };
    }

    public static AutoComponentType GetDefaultComponentForTable(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoComponentType.AutoTextTable,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoComponentType.AutoNumeric,
            not null when propType == typeof(DateTime) || propType == typeof(DateTimeOffset) => AutoComponentType.AutoDate,
            not null when propType == typeof(bool) => AutoComponentType.AutoStatus,
            not null when propType == typeof(StatusBadgeType) => AutoComponentType.AutoBadge,
            not null when propType == typeof(Ulid) => AutoComponentType.AutoId,
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
            not null when propType == typeof(DateTime) || propType == typeof(DateTimeOffset) => AutoComponentType.AutoDate,
            not null when propType == typeof(bool) => AutoComponentType.AutoStatus,
            not null when propType == typeof(Ulid) => AutoComponentType.AutoId,
            _ => AutoComponentType.AutoText
        };
    }
}