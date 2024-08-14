using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTablePropertyConfigurationBuilder<T>(ColumnConfiguration configuration)
    : AutoPropertyConfigurationBuilder<T>(configuration)
    where T : BaseDataModel
{
    public AutoTablePropertyConfigurationBuilder<T> Name(string value)
    {
        configuration.Name = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder<T> DisableColumn(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder<T> DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder<T> DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }

    public AutoTablePropertyConfigurationBuilder<T> ComponentType(string value)
    {
        configuration.ComponentType = value;
        return this;
    }
}