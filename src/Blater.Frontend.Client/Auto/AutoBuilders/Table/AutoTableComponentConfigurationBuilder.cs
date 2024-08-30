using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableComponentConfigurationBuilder(TableComponentConfiguration configuration) 
    : AutoComponentConfigurationBuilder(configuration), IAutoTableComponentConfigurationBuilder
{
    public IAutoTableComponentConfigurationBuilder Name(string value)
    {
        configuration.Name = value;
        return this;
    }

    public IAutoTableComponentConfigurationBuilder DisableColumn(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public IAutoTableComponentConfigurationBuilder DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public IAutoTableComponentConfigurationBuilder DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }
    
    public IAutoTableComponentConfigurationBuilder ComponentType(AutoTableComponentType value)
    {
        configuration.AutoComponentTypes[AutoComponentDisplayType.Table] = value;
        return this;
    }
}