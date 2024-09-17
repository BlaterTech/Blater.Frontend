using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public class AutoFormMemberConfigurationBuilder<TModel>(
    Type type, 
    AutoComponentDisplayType displayType,
    AutoFormGroupConfiguration configuration) : 
    IAutoFormMemberConfigurationBuilder<TModel>
{
    public IAutoFormMemberConfigurationBuilder<T> AddSubgroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action)
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

    public AutoFormAutoPropertyConfiguration<TProperty> AddMember<TProperty>(Expression<Func<TModel, TProperty>> expression, AutoFormAutoPropertyConfiguration<TProperty> propertyConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var index = configuration.ComponentConfigurations.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            configuration.ComponentConfigurations[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = property;
            propertyConfiguration.AutoComponentType ??= property.GetDefaultAutoFormComponentForType();
            configuration.ComponentConfigurations.Add(propertyConfiguration);
        }

        return configuration;
    }
}