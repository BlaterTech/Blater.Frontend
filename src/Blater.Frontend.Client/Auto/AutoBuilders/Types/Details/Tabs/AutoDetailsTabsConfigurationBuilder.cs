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

    public IAutoDetailsTabsMemberConfigurationBuilder AddGroup(AutoDetailsTabsGroupConfiguration detailsGroupConfiguration)
    {
        var index = _configuration.Groups.IndexOf(detailsGroupConfiguration);
        if (index != -1)
        {
            _configuration.Groups[index] = detailsGroupConfiguration;
        }
        else
        {
            _configuration.Groups.Add(detailsGroupConfiguration);
        }

        return new AutoDetailsTabsMemberConfigurationBuilder(_type, detailsGroupConfiguration);
    }
}