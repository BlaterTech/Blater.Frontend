namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsConfiguration
{
    public required string Title { get; set; }
    public string? ExtraClass { get; set; }
    public bool EnableBackButton { get; set; } = true;
    public List<AutoDetailsTabsGroupConfiguration> Groups { get; set; } = [];
}