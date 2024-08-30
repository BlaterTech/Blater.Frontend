using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsComponentConfigurationBuilder<TType>(DetailsComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder<TType>(configuration), IAutoDetailsComponentConfigurationBuilder<TType>
{
    public IAutoDetailsComponentConfigurationBuilder<TType> ComponentType(AutoDetailsComponentType value)
    {
        configuration.AutoComponentTypes[AutoComponentDisplayType.Details] = value;
        return this;
    }
}