using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BasePropertyConfiguration
{
    public PropertyInfo Property { get; set; } = null!;
    
    public string DataFormat { get; set; } = string.Empty;
    public string CssClass { get; set; } = string.Empty;
    public string CssStyle { get; set; } = string.Empty;
    public string LocalizationId { get; set; } = string.Empty;
    
    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];

    public Dictionary<AutoComponentDisplayType, BaseAutoComponentTypeEnumeration> AutoComponentTypes { get; set; } = [];
    
    public int Order { get; set; }
}