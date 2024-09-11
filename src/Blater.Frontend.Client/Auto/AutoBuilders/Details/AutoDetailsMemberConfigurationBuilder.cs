using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.Auto.Interfaces.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, AutoDetailsGroupConfiguration configuration) : IAutoDetailsMemberConfigurationBuilder
{
    public IAutoDetailsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var index = configuration.Components.IndexOf(componentConfiguration);
        if (index != -1)
        {
            configuration.Components[index] = componentConfiguration;
            return this;
        }

        componentConfiguration.Property = property;
        componentConfiguration.AutoComponentType ??= property.GetDefaultAutoDetailsComponentForType();
        configuration.Components.Add(componentConfiguration);
        return this;
    }
}