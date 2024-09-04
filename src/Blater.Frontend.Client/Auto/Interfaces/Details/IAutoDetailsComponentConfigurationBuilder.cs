using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.Interfaces.Details;

public interface IAutoDetailsComponentConfigurationBuilder<TType> : IAutoComponentConfigurationBuilder<TType>
{
    IAutoDetailsComponentConfigurationBuilder<TType> ComponentType(AutoDetailsComponentType value);
}