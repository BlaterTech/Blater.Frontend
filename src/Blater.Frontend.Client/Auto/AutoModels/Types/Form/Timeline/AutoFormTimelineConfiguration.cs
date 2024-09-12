using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineConfiguration
{
    public required string Title { get; set; }
    
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoFormTimelineActionConfiguration> ActionConfiguration { get; set; } = [];
    
    public Dictionary<AutoComponentDisplayType, List<AutoFormTimelineGroupConfiguration>> GroupConfigurations { get; set; } = [];
}