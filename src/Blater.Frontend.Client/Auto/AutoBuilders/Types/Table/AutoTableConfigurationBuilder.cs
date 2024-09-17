using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;

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

    public IAutoTableConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoTableAutoPropertyConfiguration propertyConfiguration)
    {
        var property = expression.GetPropertyInfoForType(_type);

        var index = _configuration.Configurations.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            _configuration.Configurations[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = property;
            propertyConfiguration.AutoComponentType ??= property.GetDefaultComponentForType();
            _configuration.Configurations.Add(propertyConfiguration);
        }
        
        return this;
    }
}