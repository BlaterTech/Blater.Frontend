using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, AutoFormGroupConfiguration configuration)
{
    #region Members
    
    public AutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
        => AddMember(expression, AutoComponentDisplayType.Form, componentConfiguration);

    public AutoFormMemberConfigurationBuilder AddMemberCreateOnly<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
        => AddMember(expression, AutoComponentDisplayType.FormCreate, componentConfiguration);

    public AutoFormMemberConfigurationBuilder AddMemberEditOnly<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
        => AddMember(expression, AutoComponentDisplayType.FormEdit, componentConfiguration);

    #endregion

    private AutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoComponentDisplayType displayType, AutoFormAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var formComponentConfigurations = configuration.ComponentConfigurations
                                                       .GetValueOrDefault(displayType);

        formComponentConfigurations ??= [];
        var currentComponentConfig = formComponentConfigurations.FirstOrDefault(x => x.Property == property);

        if (currentComponentConfig != null)
        {
            currentComponentConfig = componentConfiguration;
            
            if (currentComponentConfig.AutoComponentType == null)
            {
                currentComponentConfig.AutoComponentType = property.GetDefaultAutoFormComponentForType();
            }

            return this;
        }

        currentComponentConfig = componentConfiguration;
        currentComponentConfig.Property = property;
        currentComponentConfig.AutoComponentType = property.GetDefaultAutoFormComponentForType();
        formComponentConfigurations.Add(currentComponentConfig);
        configuration.ComponentConfigurations[displayType] = formComponentConfigurations;
        
        return this;
    }
}