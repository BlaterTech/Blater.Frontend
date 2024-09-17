using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Base;

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