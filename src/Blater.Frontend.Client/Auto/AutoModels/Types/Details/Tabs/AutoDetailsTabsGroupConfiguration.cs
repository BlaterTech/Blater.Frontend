using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsGroupConfiguration<TModel> : BaseAutoGroupConfiguration
{
    public AutoDetailsTabsGroupConfiguration(string title) : base(title)
    {
        Title = title;
    }

    public AutoDetailsTabsGroupConfiguration(string title, string route) : base(title)
    {
        Title = title;
        Route = route;
    }
    
    public string? Route { get; set; }
    public bool EnableEditButton { get; set; } = true;
    public List<IAutoDetailsTabsPropertyConfiguration<TModel>> Components { get; set; } = [];
}