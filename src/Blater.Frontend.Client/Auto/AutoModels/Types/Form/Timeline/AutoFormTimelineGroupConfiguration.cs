namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    
    public List<AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public List<AutoFormAutoComponentConfiguration> ComponentConfigurations { get; set; } = [];
}