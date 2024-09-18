using System.Reflection;
using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseAutoPropertyConfiguration<TPropertyValue> :
    IBaseAutoPropertyConfigurationValue<TPropertyValue>
{
    public PropertyInfo Property { get; set; } = null!;

    public string DataFormat { get; set; } = string.Empty;
    public string Mask { get; set; } = string.Empty;
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
    public BaseAutoComponentTypeEnumeration? AutoComponentType { get; set; }
    public AutoFieldSize Size { get; set; }
    
    public TPropertyValue? Value { get; set; }
    
    public EventCallback<TPropertyValue> OnValueChanged { get; set; }
    public EventCallback<EventArgs> OnClick { get; set; }
}