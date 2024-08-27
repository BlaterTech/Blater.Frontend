using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTablePropertyConfigurationBuilder(TablePropertyConfiguration configuration)
    : AutoPropertyConfigurationBuilder(configuration)
{
    public AutoTablePropertyConfigurationBuilder Name(string value)
    {
        configuration.Name = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder DisableColumn(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder ComponentType(string value)
    {
        configuration.ComponentType = value;
        return this;
    }
}