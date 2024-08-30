using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoDetailsComponentConfigurationBuilder<TProperty> : IAutoComponentConfigurationBuilder
{
    IAutoDetailsComponentConfigurationBuilder<TProperty> ComponentType(AutoDetailsComponentType value);
}