using Blater.Frontend.Client.Auto.AutoModels;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoTablePropertyConfigurationBuilder : IAutoPropertyConfigurationBuilder
{
    IAutoTablePropertyConfigurationBuilder Name(string value);

    IAutoTablePropertyConfigurationBuilder DisableColumn(bool value = false);

    IAutoTablePropertyConfigurationBuilder DisableFilter(bool value = false);

    IAutoTablePropertyConfigurationBuilder DisableSortBy(bool value = false);

    IAutoTablePropertyConfigurationBuilder ComponentType(AutoTableComponentType value);
}