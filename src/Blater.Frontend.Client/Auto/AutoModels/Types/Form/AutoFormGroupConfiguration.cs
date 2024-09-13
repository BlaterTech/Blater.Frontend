namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormGroupConfiguration
{
    public AutoFormGroupConfiguration(string title)
    {
        Title = title;
    }
    
    public string Title { get; set; }
    
    public List<AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public List<AutoFormAutoComponentConfiguration> ComponentConfigurations { get; set; } = [];
}