namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsConfiguration<TModel>(string title)
{
    public string Title { get; set; } = title;
    public string? ExtraClass { get; set; }
    public bool EnableBackButton { get; set; } = true;
    public List<AutoDetailsTabsPanelConfiguration<TModel>> PanelConfigurations { get; set; } = [];
}