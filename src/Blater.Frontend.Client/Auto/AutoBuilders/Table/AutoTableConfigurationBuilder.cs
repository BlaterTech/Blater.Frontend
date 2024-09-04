using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableConfigurationBuilder(Type type, AutoTableConfiguration configuration)
{
    public AutoTableConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoTableAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);

        var index = configuration.Configurations.IndexOf(componentConfiguration);
        if (index != -1)
        {
            configuration.Configurations[index] = componentConfiguration;
            return this;
        }

        componentConfiguration.Property = property;
        componentConfiguration.AutoComponentType ??= property.GetDefaultAutoTableComponentForType();
        
        configuration.Configurations.Add(componentConfiguration);

        return this;
    }
}