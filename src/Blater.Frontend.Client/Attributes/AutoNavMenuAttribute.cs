namespace Blater.Frontend.Client.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AutoNavMenuAttribute(int order, string? icon = null) : Attribute
{
    public AutoNavMenuAttribute() : this(1)
    {
    }
    
    public AutoNavMenuAttribute(string icon) : this(1, icon)
    {
    }

    public int Order { get; set; } = order;

    public string? Icon { get; set; } = icon;

    public string? NavMenuParentName { get; set; }
}