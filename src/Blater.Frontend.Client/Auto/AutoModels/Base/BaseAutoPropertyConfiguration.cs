using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Globalization;
using System.Reflection;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseAutoPropertyConfiguration<TPropertyValue> : IBaseAutoPropertyConfigurationValue<TPropertyValue>
{
    public PropertyInfo Property { get; set; } = null!;

    public string? DataFormat { get; set; }
    public string? Mask { get; set; }
    public string? ExtraClass { get; set; }
    public string? ExtraStyle { get; set; }
    public string? LabelNameLocalizationId { get; set; }
    public string? PlaceHolderLocalizationId { get; set; }
    public string? LabelName { get; set; }
    public string? Placeholder { get; set; }
    public string? HelpMessage { get; set; }

    public bool IsReadOnly { get; set; }
    public bool EnableDefaultValue { get; set; }
    public bool Disable { get; set; }

    public int Order { get; set; }

    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];
    public BaseAutoComponentTypeEnumeration? AutoComponentType { get; set; }
    public AutoFieldSize Size { get; set; }
    public CultureInfo Culture { get; set; } = new("pt-BR");

    public TPropertyValue? Value { get; set; }

    public EventCallback<TPropertyValue> OnValueChanged { get; set; }
    public EventCallback<EventArgs> OnClick { get; set; }
}