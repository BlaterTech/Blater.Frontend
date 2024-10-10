using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;
using Blater.Frontend.Client.Contracts.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public partial class AutoDetailsTabsBuilder<T> : AutoDetailsBuilder<T> where T : BaseFrontendModel
{
    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.DetailsTabs;
    public override bool HasLabel { get; set; }

    private AutoDetailsTabsConfiguration<T> DetailsTabsConfiguration { get; set; } = new("Default");
    protected override void LoadModelConfig()
    {
        var autoDetails = FindModelConfig<IAutoDetailsTabsConfiguration<T>>();
        DetailsTabsConfiguration = autoDetails.DetailsTabsConfiguration;
    }
}