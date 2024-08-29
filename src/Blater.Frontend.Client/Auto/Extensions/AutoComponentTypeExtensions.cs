using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Base;

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
}