namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsGroupConfiguration
{
    public AutoDetailsTabsGroupConfiguration(string title)
    {
        Title = title;
    }

    public AutoDetailsTabsGroupConfiguration(string title, string route)
    {
        Title = title;
        Route = route;
    }
    
    public string Title { get; set; }
    public string? Route { get; set; }
    public bool EnableEditButton { get; set; } = true;
    public List<AutoDetailsTabsComponentConfiguration> Components { get; set; } = [];
}