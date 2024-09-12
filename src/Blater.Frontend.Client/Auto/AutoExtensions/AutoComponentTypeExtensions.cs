using System.Linq.Expressions;
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
        
        return componentType.GetType() == typeof(AutoFormComponentInputType);
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
    
    public static AutoFormComponentInputType GetDefaultAutoFormComponentForType(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoFormComponentInputType.AutoTextComponentInput,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoFormComponentInputType.AutoNumericComponentInput,
            not null when propType == typeof(DateTime) => AutoFormComponentInputType.AutoDateTimeComponentInput,
            not null when propType == typeof(bool) => AutoFormComponentInputType.AutoSwitchComponentInput,
            _ => AutoFormComponentInputType.AutoTextComponentInput
        };
    }
    
    public static AutoDetailsComponentType GetDefaultAutoDetailsComponentForType(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoDetailsComponentType.AutoText,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoDetailsComponentType.AutoNumeric,
            not null when propType == typeof(DateTime) => AutoDetailsComponentType.AutoDate,
            not null when propType == typeof(bool) => AutoDetailsComponentType.AutoStatus,
            _ => AutoDetailsComponentType.AutoText
        };
    }
    
    public static AutoTableComponentType GetDefaultAutoTableComponentForType(this PropertyInfo propertyInfo)
    {
        var propType = propertyInfo.PropertyType;
        return propType switch
        {
            not null when propType == typeof(string) => AutoTableComponentType.AutoTextTable,
            not null when propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal) => AutoTableComponentType.AutoNumeric,
            not null when propType == typeof(DateTime) => AutoTableComponentType.AutoDate,
            not null when propType == typeof(bool) => AutoTableComponentType.AutoStatus,
            _ => AutoTableComponentType.AutoTextTable
        };
    }
}