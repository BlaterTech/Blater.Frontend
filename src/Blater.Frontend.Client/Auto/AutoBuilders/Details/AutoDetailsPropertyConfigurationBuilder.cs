using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsPropertyConfigurationBuilder<TProperty>(AutoComponentConfiguration configuration)
    : AutoPropertyConfigurationBuilder(configuration), IAutoDetailsPropertyConfigurationBuilder<TProperty>
{
    private readonly AutoComponentConfiguration _configuration = configuration;

    public IAutoDetailsPropertyConfigurationBuilder<TProperty> ComponentType(AutoDetailsComponentType value)
    {
        _configuration.AutoComponentTypes[AutoComponentDisplayType.Details] = value;
        return this;
    }
}