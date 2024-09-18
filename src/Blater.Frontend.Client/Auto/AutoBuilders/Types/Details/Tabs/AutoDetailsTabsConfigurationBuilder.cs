using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsConfigurationBuilder<TModel> : IAutoDetailsTabsConfigurationBuilder<TModel>
{
    private readonly AutoDetailsTabsConfiguration<TModel> _configuration;

    public AutoDetailsTabsConfigurationBuilder(object instance)
    {
        if (instance is IAutoDetailsTabsConfiguration<TModel> configuration)
        {
            _configuration = configuration.DetailsTabsConfiguration;
        }
        else
        {
            throw new InvalidCastException($"Instance is not implement IAutoDetailsTabsConfiguration<{typeof(TModel).Name}>");
        }
    }

    public AutoDetailsTabsPanelConfiguration<TModel> AddPanel(AutoDetailsTabsPanelConfiguration<TModel> tabsPanelConfiguration, Action<IAutoDetailsTabsGroupConfigurationBuilder<TModel>> action)
    {
        var index = _configuration.PanelConfigurations.IndexOf(tabsPanelConfiguration);
        if (index != -1)
        {
            _configuration.PanelConfigurations[index] = tabsPanelConfiguration;
        }
        else
        {
            _configuration.PanelConfigurations.Add(tabsPanelConfiguration);
        }
        
        var builder = new AutoDetailsTabsGroupConfigurationBuilder<TModel>(tabsPanelConfiguration);
        
        action.Invoke(builder);

        return tabsPanelConfiguration;
    }
}