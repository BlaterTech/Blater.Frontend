using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsConfigurationBuilder<TModel> : IAutoDetailsTabsConfigurationBuilder<TModel>
{
    private readonly AutoDetailsTabsConfiguration<TModel> _configuration;

    public AutoDetailsTabsConfigurationBuilder(object instance)
    {
        if (instance is IAutoDetailsTabsConfiguration<TModel> configuration)
        {
            _configuration = configuration.DetailsTabsConfiguration;
            _configuration.LocalizationId ??= $"Blater-AutoDetailsTabs-{typeof(TModel).Name}";
        }
        else
        {
            throw new InvalidCastException($"Instance is not implement IAutoDetailsTabsConfiguration<{typeof(TModel).Name}>");
        }
    }

    public AutoDetailsTabsPanelConfiguration<TModel> AddPanel(string title, Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action)
        => AddPanel(new AutoDetailsTabsPanelConfiguration<TModel>(title), action);

    public AutoDetailsTabsPanelConfiguration<TModel> AddPanel(string title, Icons icon, Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action)
        => AddPanel(new AutoDetailsTabsPanelConfiguration<TModel>(title, icon), action);

    public AutoDetailsTabsPanelConfiguration<TModel> AddPanel(AutoDetailsTabsPanelConfiguration<TModel> tabsPanelConfiguration,
                                                              Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action)
    {
        var index = _configuration.Panels.IndexOf(tabsPanelConfiguration);
        if (index != -1)
        {
            _configuration.Panels[index] = tabsPanelConfiguration;
        }
        else
        {
            tabsPanelConfiguration.LocalizationId ??= $"Blater-AutoDetailsTabs-{typeof(TModel).Name}-Panel";
            if (string.IsNullOrWhiteSpace(tabsPanelConfiguration.Title))
            {
                throw new Exception("Details panel title is null or white space");
            }
            _configuration.Panels.Add(tabsPanelConfiguration);
        }

        var builder = new AutoDetailsTabsGroupConfigurationBuilder<TModel>(tabsPanelConfiguration);

        action.Invoke(builder);

        return tabsPanelConfiguration;
    }
}