using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsConfigurationBuilder
{
    IAutoDetailsTabsGroupConfigurationBuilder AddPanel(AutoDetailsTabsPanelConfiguration tabsPanelConfiguration);
}