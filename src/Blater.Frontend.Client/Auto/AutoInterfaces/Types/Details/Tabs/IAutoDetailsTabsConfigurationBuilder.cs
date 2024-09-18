using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsConfigurationBuilder<TModel>
{
    AutoDetailsTabsPanelConfiguration<TModel> AddPanel(AutoDetailsTabsPanelConfiguration<TModel> tabsPanelConfiguration, Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action);
}