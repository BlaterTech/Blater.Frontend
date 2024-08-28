using Blater.Frontend.Client.Auto.AutoModels;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoDetailsPropertyConfigurationBuilder<TProperty> : IAutoPropertyConfigurationBuilder
{
    IAutoDetailsPropertyConfigurationBuilder<TProperty> ComponentType(AutoDetailsComponentType value);
}