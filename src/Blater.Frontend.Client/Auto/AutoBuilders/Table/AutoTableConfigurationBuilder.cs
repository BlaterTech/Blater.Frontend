using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.Auto.Interfaces.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableConfigurationBuilder : IAutoTableConfigurationBuilder
{
    private readonly AutoTableConfiguration _configuration;
    private readonly Type _type;

    public AutoTableConfigurationBuilder(Type type, object instance)
    {
        _type = type;
        if (instance is IAutoTableConfiguration configuration)
        {
            _configuration = configuration.TableConfiguration;
        }
        else
        {
            throw new InvalidCastException("Instance is not implement IAutoTableConfiguration");
        }
    }

    public IAutoTableConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoTableAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(_type);

        var index = _configuration.Configurations.IndexOf(componentConfiguration);
        if (index != -1)
        {
            _configuration.Configurations[index] = componentConfiguration;
        }
        else
        {
            componentConfiguration.Property = property;
            componentConfiguration.AutoComponentType ??= property.GetDefaultAutoTableComponentForType();
            _configuration.Configurations.Add(componentConfiguration);
        }
        
        return this;
    }
}