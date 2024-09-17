using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormGroupConfiguration
{
    public AutoFormGroupConfiguration(string title)
    {
        Title = title;
    }
    
    public string Title { get; set; }
    
    public List<AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public List<IBaseAutoPropertyConfiguration> ComponentConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, List<AutoFormGroupConfiguration>> SubGroups { get; set; } = [];
}