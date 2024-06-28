using System.Reflection;

namespace Blater.Frontend.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class ShortNameAttribute(string shortName) : Attribute
{
    public string ShortName { get; } = shortName;
}

public static class ShortNameAttributeExtensions
{
    public static string GetShortName(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        var result = type.GetField(name!)?.GetCustomAttribute<ShortNameAttribute>()?.ShortName;
        
        if (result != null)
        {
            return result;
        }
        
        throw new Exception($"ShortNameAttribute not found for {value}");
    }
}