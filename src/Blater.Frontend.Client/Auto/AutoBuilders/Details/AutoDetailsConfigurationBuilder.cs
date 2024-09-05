using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.Interfaces.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsConfigurationBuilder : IAutoDetailsConfigurationBuilder
{
    private readonly AutoDetailsConfiguration _configuration;
    private readonly Type _type;

    public AutoDetailsConfigurationBuilder(Type type, object instance)
    {
        _type = type;
        if (instance is IAutoDetailsConfiguration configuration)
        {
            _configuration = configuration.DetailsConfiguration;
        }
        else
        {
            throw new InvalidCastException("Instance is not implement IAutoDetailsConfiguration");
        }
    }

    public IAutoDetailsMemberConfigurationBuilder AddGroup(AutoDetailsGroupConfiguration detailsGroupConfiguration)
    {
        var index = _configuration.Configurations.IndexOf(detailsGroupConfiguration);
        if (index != -1)
        {
            _configuration.Configurations[index] = detailsGroupConfiguration;
        }
        else
        {
            _configuration.Configurations.Add(detailsGroupConfiguration);
        }

        return new AutoDetailsMemberConfigurationBuilder(_type, detailsGroupConfiguration);
    }
}