using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class AutoFormGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, List<AutoFormComponentConfiguration>> ComponentConfigurations { get; set; } = [];
}