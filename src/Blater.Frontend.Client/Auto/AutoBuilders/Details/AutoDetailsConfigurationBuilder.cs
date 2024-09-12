using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Details;

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
        var index = _configuration.Groups.IndexOf(detailsGroupConfiguration);
        if (index != -1)
        {
            _configuration.Groups[index] = detailsGroupConfiguration;
        }
        else
        {
            _configuration.Groups.Add(detailsGroupConfiguration);
        }

        return new AutoDetailsMemberConfigurationBuilder(_type, detailsGroupConfiguration);
    }
}