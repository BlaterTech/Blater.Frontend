using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoTableComponentConfigurationBuilder : IAutoComponentConfigurationBuilder
{
    IAutoTableComponentConfigurationBuilder Name(string value);

    IAutoTableComponentConfigurationBuilder DisableColumn(bool value = false);

    IAutoTableComponentConfigurationBuilder DisableFilter(bool value = false);

    IAutoTableComponentConfigurationBuilder DisableSortBy(bool value = false);

    IAutoTableComponentConfigurationBuilder ComponentType(AutoTableComponentType value);
}