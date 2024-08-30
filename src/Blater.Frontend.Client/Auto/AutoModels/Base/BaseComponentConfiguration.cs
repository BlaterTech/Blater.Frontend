using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseComponentConfiguration
{
    public PropertyInfo Property { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    public string DataFormat { get; set; } = string.Empty;
    public string ExtraClass { get; set; } = string.Empty;
    public string ExtraStyle { get; set; } = string.Empty;
    public string LocalizationId { get; set; } = string.Empty;
    public string LabelName { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    public string HelpMessage { get; set; } = string.Empty;
    
    public bool IsReadOnly { get; set; }
    public bool Disable { get; set; }
    
    public int Order { get; set; }
    
    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, BaseAutoComponentTypeEnumeration> AutoComponentTypes { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoFieldSize> Sizes { get; set; } = [];
    
    public EventCallback<object> OnValueChanged { get; set; }
    public EventCallback<object> OnParameterSet { get; set; }
    public EventCallback<MouseEventArgs> OnClick { get; set; }
}