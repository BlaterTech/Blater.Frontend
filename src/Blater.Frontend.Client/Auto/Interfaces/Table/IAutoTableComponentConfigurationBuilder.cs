using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.Interfaces.Table;

public interface IAutoTableComponentConfigurationBuilder<TType> : IAutoComponentConfigurationBuilder<TType>
{
    IAutoTableComponentConfigurationBuilder<TType> Name(string value);

    IAutoTableComponentConfigurationBuilder<TType> DisableColumn(bool value = false);

    IAutoTableComponentConfigurationBuilder<TType> DisableFilter(bool value = false);

    IAutoTableComponentConfigurationBuilder<TType> DisableSortBy(bool value = false);

    IAutoTableComponentConfigurationBuilder<TType> ComponentType(AutoTableComponentType value);
}