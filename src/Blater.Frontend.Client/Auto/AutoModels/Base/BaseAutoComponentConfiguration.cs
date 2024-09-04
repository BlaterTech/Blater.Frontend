using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseAutoComponentConfiguration : BaseAutoEventConfiguration
{
    public PropertyInfo Property { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string DataFormat { get; set; } = string.Empty;
    public string Mask { get; set; } = string.Empty;
    public string ExtraClass { get; set; } = string.Empty;
    public string ExtraStyle { get; set; } = string.Empty;
    public string LocalizationId { get; set; } = string.Empty;
    public required string LabelName { get; set; }
    public string Placeholder { get; set; } = string.Empty;
    public string HelpMessage { get; set; } = string.Empty;

    public bool IsReadOnly { get; set; }
    public bool Disable { get; set; }

    public int Order { get; set; }

    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];
    public BaseAutoComponentTypeEnumeration? AutoComponentType { get; set; }
    public AutoFieldSize Size { get; set; }
}