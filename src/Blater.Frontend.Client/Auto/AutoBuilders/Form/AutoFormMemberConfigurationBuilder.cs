using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(
    Type type, 
    AutoFormGroupConfiguration configuration) : 
    IAutoFormMemberConfigurationBuilder
{
    public IAutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);

        var currentComponentConfig = configuration.ComponentConfigurations.FirstOrDefault(x => x.Property == property);

        if (currentComponentConfig != null && currentComponentConfig.AutoComponentType == null)
        {
            currentComponentConfig = componentConfiguration;
            currentComponentConfig.AutoComponentType = property.GetDefaultAutoFormComponentForType();

            return this;
        }

        currentComponentConfig = componentConfiguration;
        currentComponentConfig.Property = property;
        currentComponentConfig.AutoComponentType ??= property.GetDefaultAutoFormComponentForType();
        configuration.ComponentConfigurations.Add(currentComponentConfig);
        
        return this;
    }
}