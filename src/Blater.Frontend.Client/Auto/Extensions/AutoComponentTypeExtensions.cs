using System.Linq.Expressions;
using System.Reflection;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.Extensions;

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
    
    public static AutoFormComponentInputType GetDefaultAutoFormComponentForType(this Type propertyType)
    {
        return propertyType switch
        {
            not null when propertyType == typeof(string) => AutoFormComponentInputType.AutoTextComponentInput,
            not null when propertyType == typeof(int) || propertyType == typeof(double) || propertyType == typeof(decimal) => AutoFormComponentInputType.AutoNumericComponentInput,
            not null when propertyType == typeof(DateTime) => AutoFormComponentInputType.AutoDateTimeComponentInput,
            not null when propertyType == typeof(bool) => AutoFormComponentInputType.AutoSwitchComponentInput,
            _ => AutoFormComponentInputType.AutoTextComponentInput
        };
    }
    
    public static AutoFormComponentInputType GetDefaultAutoDetailsComponentForType(this Type propertyType)
    {
        return propertyType switch
        {
            not null when propertyType == typeof(string) => AutoFormComponentInputType.AutoTextComponentInput,
            not null when propertyType == typeof(int) || propertyType == typeof(double) || propertyType == typeof(decimal) => AutoFormComponentInputType.AutoNumericComponentInput,
            not null when propertyType == typeof(DateTime) => AutoFormComponentInputType.AutoDateTimeComponentInput,
            not null when propertyType == typeof(bool) => AutoFormComponentInputType.AutoSwitchComponentInput,
            _ => AutoFormComponentInputType.AutoTextComponentInput
        };
    }
    
    public static AutoFormComponentInputType GetDefaultAutoTableComponentForType(this Type propertyType)
    {
        return propertyType switch
        {
            not null when propertyType == typeof(string) => AutoFormComponentInputType.AutoTextComponentInput,
            not null when propertyType == typeof(int) || propertyType == typeof(double) || propertyType == typeof(decimal) => AutoFormComponentInputType.AutoNumericComponentInput,
            not null when propertyType == typeof(DateTime) => AutoFormComponentInputType.AutoDateTimeComponentInput,
            not null when propertyType == typeof(bool) => AutoFormComponentInputType.AutoSwitchComponentInput,
            _ => AutoFormComponentInputType.AutoTextComponentInput
        };
    }
}