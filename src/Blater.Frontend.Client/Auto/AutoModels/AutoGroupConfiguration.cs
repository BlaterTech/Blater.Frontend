using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    
    public bool DisableEditButton { get; set; }
    
    public Dictionary<AutoComponentDisplayType, List<AutoComponentConfiguration>> Configurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoFieldSize> Sizes { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration> GridConfigurations { get; set; } = [];
    
    public AutoActionConfiguration AutoActionConfiguration { get; set; } = new();
    public AutoAvatarConfiguration AutoAvatarConfiguration { get; set; } = new();
}