using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsGroupConfigurationBuilder<TModel>(AutoDetailsTabsPanelConfiguration configuration) : IAutoDetailsTabsGroupConfigurationBuilder<TModel>
{
    public AutoDetailsTabsGroupConfiguration AddGroup(AutoDetailsTabsGroupConfiguration groupConfiguration, Action<IAutoDetailsTabsMemberConfigurationBuilder<TModel>> memberConfiguration)
    {
        var index = configuration.GroupConfigurations.IndexOf(groupConfiguration);
        if (index != -1)
        {
            configuration.GroupConfigurations[index] = groupConfiguration;
        }
        else
        {
            configuration.GroupConfigurations.Add(groupConfiguration);
        }
        
        var builder = new AutoDetailsTabsPropertyConfigurationBuilder<TModel>(groupConfiguration);
        
        memberConfiguration.Invoke(builder);

        return groupConfiguration;
    }
}