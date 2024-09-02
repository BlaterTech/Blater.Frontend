using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, List<FormComponentConfiguration>> ComponentConfigurations { get; set; } = [];
}