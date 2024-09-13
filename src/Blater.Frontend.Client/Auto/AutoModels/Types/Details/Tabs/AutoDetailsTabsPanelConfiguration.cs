using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsPanelConfiguration
{
    public required string Title { get; set; } = string.Empty;
    public Icons? Icon { get; set; }
    public List<AutoDetailsTabsGroupConfiguration> GroupConfigurations { get; set; } = [];
}