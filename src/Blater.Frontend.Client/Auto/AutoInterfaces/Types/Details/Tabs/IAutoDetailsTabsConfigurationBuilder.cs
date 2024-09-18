using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsConfigurationBuilder<TModel>
{
    AutoDetailsTabsPanelConfiguration<TModel> AddPanel(string title, Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action);
    AutoDetailsTabsPanelConfiguration<TModel> AddPanel(string title, Icons icon, Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action);
    AutoDetailsTabsPanelConfiguration<TModel> AddPanel(AutoDetailsTabsPanelConfiguration<TModel> tabsPanelConfiguration, Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action);
}