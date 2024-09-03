using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Table;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableComponentConfigurationBuilder<TType>(AutoTableComponentConfiguration configuration) 
    : AutoComponentConfigurationBuilder<TType>(configuration), IAutoTableComponentConfigurationBuilder<TType>
{
    public IAutoTableComponentConfigurationBuilder<TType> Name(string value)
    {
        configuration.Name = value;
        return this;
    }

    public IAutoTableComponentConfigurationBuilder<TType> DisableColumn(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public IAutoTableComponentConfigurationBuilder<TType> DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public IAutoTableComponentConfigurationBuilder<TType> DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }
    
    public IAutoTableComponentConfigurationBuilder<TType> ComponentType(AutoTableComponentType value)
    {
        configuration.AutoComponentTypes[AutoComponentDisplayType.Table] = value;
        return this;
    }
}