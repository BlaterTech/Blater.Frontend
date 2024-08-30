using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsComponentConfigurationBuilder<TProperty>(DetailsComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder(configuration), IAutoDetailsComponentConfigurationBuilder<TProperty>
{
    public IAutoDetailsComponentConfigurationBuilder<TProperty> ComponentType(AutoDetailsComponentType value)
    {
        configuration.AutoComponentTypes[AutoComponentDisplayType.Details] = value;
        return this;
    }
}