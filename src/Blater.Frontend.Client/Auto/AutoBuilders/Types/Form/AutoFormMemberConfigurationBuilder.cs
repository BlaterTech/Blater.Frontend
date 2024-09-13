using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public class AutoFormMemberConfigurationBuilder(
    Type type, 
    AutoFormGroupConfiguration configuration) : 
    IAutoFormMemberConfigurationBuilder
{
    public IAutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var index = configuration.ComponentConfigurations.IndexOf(componentConfiguration);
        if (index != -1)
        {
            configuration.ComponentConfigurations[index] = componentConfiguration;
        }
        else
        {
            componentConfiguration.Property = property;
            componentConfiguration.AutoComponentType ??= property.GetDefaultAutoFormComponentForType();
            configuration.ComponentConfigurations.Add(componentConfiguration);
        }
        
        return this;
    }
}