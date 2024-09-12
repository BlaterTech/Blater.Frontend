namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsGroupConfiguration
{
    public required string Title { get; set; } = string.Empty;
    public bool EnableEditButton { get; set; } = true;
    public List<AutoDetailsTabsComponentConfiguration> Components { get; set; } = [];   
}