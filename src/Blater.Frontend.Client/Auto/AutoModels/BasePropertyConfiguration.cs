using System.Reflection;

namespace Blater.Frontend.Client.Auto.AutoModels;

public abstract class BasePropertyConfiguration
{
    public PropertyInfo PropertyInfo { get; init; } = null!;
    
    public string DataFormat { get; set; } = string.Empty;
    public string CssClass { get; set; } = string.Empty;
    public string CssStyle { get; set; } = string.Empty;
    
    public int Order { get; set; }
}