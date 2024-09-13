using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsGroupConfigurationBuilder(Type type, AutoDetailsTabsPanelConfiguration configuration) : IAutoDetailsTabsGroupConfigurationBuilder
{
    public IAutoDetailsTabsMemberConfigurationBuilder AddGroup(AutoDetailsTabsGroupConfiguration groupConfiguration)
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

        return new AutoDetailsTabsMemberConfigurationBuilder(type, groupConfiguration);
    }
}