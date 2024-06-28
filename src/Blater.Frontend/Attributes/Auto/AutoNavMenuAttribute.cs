namespace Blater.Frontend.Attributes.Auto;

[AttributeUsage(AttributeTargets.Class)]
public class AutoNavMenuAttribute : Attribute
{
    public AutoNavMenuAttribute()
    {
        Order = 1;
    }
    
    public AutoNavMenuAttribute(string icon)
    {
        Order = 1;
        Icon = icon;
    }
    
    public AutoNavMenuAttribute(int order, string? icon = null)
    {
        Order = order;
        Icon = icon;
    }
    
    public int Order { get; set; }
    
    public string? Icon { get; set; }
    
    public string? NavMenuParentName { get; set; }
}