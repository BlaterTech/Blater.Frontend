using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTablePropertyConfigurationBuilder(TablePropertyConfiguration configuration) 
    : AutoPropertyConfigurationBuilder(configuration), IAutoTablePropertyConfigurationBuilder
{
    public IAutoTablePropertyConfigurationBuilder Name(string value)
    {
        configuration.Name = value;
        return this;
    }

    public IAutoTablePropertyConfigurationBuilder DisableColumn(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public IAutoTablePropertyConfigurationBuilder DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public IAutoTablePropertyConfigurationBuilder DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }
    
    public IAutoTablePropertyConfigurationBuilder ComponentType(AutoTableComponentType value)
    {
        configuration.AutoComponentTypes[AutoComponentDisplayType.Table] = value;
        return this;
    }
}