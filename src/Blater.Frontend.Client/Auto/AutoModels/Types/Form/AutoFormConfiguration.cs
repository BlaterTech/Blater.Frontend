using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormConfiguration
{
    public required string Title { get; set; }
    
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoAvatarModelConfiguration> AvatarConfiguration { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoFormActionConfiguration> ActionConfiguration { get; set; } = [];
    
    public Dictionary<AutoComponentDisplayType, List<AutoFormGroupConfiguration>> GroupConfigurations { get; set; } = [];
}