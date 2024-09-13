using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsConfigurationBuilder : IAutoDetailsTabsConfigurationBuilder
{
    private readonly AutoDetailsTabsConfiguration _configuration;
    private readonly Type _type;

    public AutoDetailsTabsConfigurationBuilder(Type type, object instance)
    {
        _type = type;
        if (instance is IAutoDetailsTabsConfiguration configuration)
        {
            _configuration = configuration.DetailsTabsConfiguration;
        }
        else
        {
            throw new InvalidCastException("Instance is not implement IAutoDetailsTabsConfiguration");
        }
    }

    public IAutoDetailsTabsConfigurationBuilder AddPanel(AutoDetailsTabsPanelConfiguration tabsPanelConfiguration, Action<IAutoDetailsTabsGroupConfigurationBuilder> action)
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
        
        var builder = new AutoDetailsTabsGroupConfigurationBuilder(_type, tabsPanelConfiguration);
        
        action.Invoke(builder);

        return this;
    }
}