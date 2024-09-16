using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public class AutoFormMemberConfigurationBuilder(
    Type type, 
    AutoComponentDisplayType displayType,
    AutoFormGroupConfiguration configuration) : 
    IAutoFormMemberConfigurationBuilder
{
    public IAutoFormMemberConfigurationBuilder AddSubgroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action)
    {
        if (!configuration.SubGroups.TryGetValue(displayType, out var value))
        {
            value ??= [];
            configuration.SubGroups.TryAdd(displayType, value);
        }

        var index = value.IndexOf(groupConfiguration);
        if (index != -1)
        {
            value[index] = groupConfiguration;
        }
        else
        {
            value.Add(groupConfiguration);
        }

        configuration.SubGroups[displayType] = value;
        
        var builder = new AutoFormMemberConfigurationBuilder(type, displayType, groupConfiguration);
        
        action.Invoke(builder);

        return this;
    }
    
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