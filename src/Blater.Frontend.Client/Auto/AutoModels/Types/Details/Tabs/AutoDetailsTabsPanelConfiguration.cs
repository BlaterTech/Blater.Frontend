using Blater.Frontend.Client.Auto.AutoModels.Base;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsPanelConfiguration<TModel> : BaseAutoConfiguration
{
    public AutoDetailsTabsPanelConfiguration(string title) : base(title)
    {
        Title = title;
    }
    
    public AutoDetailsTabsPanelConfiguration(string title, Icons icon) : base(title)
    {
        Title = title;
        Icon = icon;
    }
    
    public Icons? Icon { get; set; }
    public List<AutoDetailsTabsGroupConfiguration<TModel>> Groups { get; set; } = [];
}