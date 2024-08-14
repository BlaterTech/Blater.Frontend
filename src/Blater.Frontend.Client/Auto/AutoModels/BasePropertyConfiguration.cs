using System.Reflection;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels;

public abstract class BasePropertyConfiguration
{
    public PropertyInfo PropertyInfo { get; set; } = null!;
    
    public string DataFormat { get; set; } = string.Empty;
    public string CssClass { get; set; } = string.Empty;
    public string CssStyle { get; set; } = string.Empty;
    public string LocalizationId { get; set; } = string.Empty;
    public string ComponentType { get; set; } = string.Empty;
    
    
    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];
    
    public int Order { get; set; }
}