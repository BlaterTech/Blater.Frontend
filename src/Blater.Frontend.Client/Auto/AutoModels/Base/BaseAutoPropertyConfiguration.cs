using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseAutoPropertyConfiguration<TPropertyValue> : IBaseAutoPropertyConfiguration, IAutoPropertyConfigurationValue<TPropertyValue>
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
}

public interface IBaseAutoPropertyConfiguration
{
    PropertyInfo Property { get; set; }
    string DataFormat { get; set; }
    string Mask { get; set; }
    string ExtraClass { get; set; }
    string ExtraStyle { get; set; }
    string LocalizationId { get; set; }
    string LabelName { get; set; }
    string Placeholder { get; set; }
    string HelpMessage { get; set; }
    bool IsReadOnly { get; set; }
    bool Disable { get; set; }
    int Order { get; set; }
    Dictionary<Breakpoint, int> Breakpoints { get; set; }
    BaseAutoComponentTypeEnumeration? AutoComponentType { get; set; }
    AutoFieldSize Size { get; set; }
}

public interface IAutoPropertyConfigurationValue<TPropertyValue>
{
    TPropertyValue? Value { get; set; }
    
    /*public TPropertyValue? Value { 
        get => Property.GetValue(this) as TPropertyValue;
        set => Property.SetValue(this, value);
    }*/
}