using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsConfiguration<TModel>
{
    AutoDetailsTabsConfiguration DetailsTabsConfiguration { get; }
    void ConfigureDetailsTabs(AutoDetailsTabsConfigurationBuilder<TModel> builder);
}