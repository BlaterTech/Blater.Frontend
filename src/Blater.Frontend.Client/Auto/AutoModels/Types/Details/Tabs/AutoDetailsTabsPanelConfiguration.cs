using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsPanelConfiguration<TModel>
{
    public AutoDetailsTabsPanelConfiguration(string title)
    {
        Title = title;
    }
    
    public AutoDetailsTabsPanelConfiguration(string title, Icons icon)
    {
        Title = title;
        Icon = icon;
    }
    
    public string Title { get; init; }
    public Icons? Icon { get; set; }
    public List<AutoDetailsTabsGroupConfiguration<TModel>> GroupConfigurations { get; set; } = [];
}