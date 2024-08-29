using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoComponentConfiguration
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
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
    
    public int Order { get; set; }
    
    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, BaseAutoComponentTypeEnumeration> AutoComponentTypes { get; set; } = [];
    
    public EventCallback<object> OnValueChanged { get; set; }
    public EventCallback<object> OnParameterSet { get; set; }
    public EventCallback<MouseEventArgs> OnClick { get; set; }
    
    public AbstractValidator<object>? Validator { get; set; }
}