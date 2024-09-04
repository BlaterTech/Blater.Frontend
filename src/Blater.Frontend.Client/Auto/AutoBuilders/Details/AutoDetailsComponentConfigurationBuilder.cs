using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsComponentConfigurationBuilder<TType>(AutoDetailsAutoComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder<TType>(configuration), IAutoDetailsComponentConfigurationBuilder<TType>
{
    public IAutoDetailsComponentConfigurationBuilder<TType> ComponentType(AutoDetailsComponentType value)
    {
        configuration.AutoComponentType = value;
        return this;
    }
}