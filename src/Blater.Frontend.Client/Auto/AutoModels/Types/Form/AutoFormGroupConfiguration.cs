namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    
    public List<AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public List<AutoFormAutoComponentConfiguration> ComponentConfigurations { get; set; } = [];
}