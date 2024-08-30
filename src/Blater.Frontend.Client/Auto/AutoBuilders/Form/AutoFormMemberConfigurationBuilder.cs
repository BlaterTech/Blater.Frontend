using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, FormGroupConfiguration configuration)
{
    public AutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, Action<AutoFormComponentConfigurationBuilder<TType>> action)
    {
        AddMember(expression, action, AutoComponentDisplayType.FormCreate);
        AddMember(expression, action, AutoComponentDisplayType.FormEdit);
        return this;
    }
    
    public AutoFormMemberConfigurationBuilder AddMemberCreateOnly<TType>(Expression<Func<TType>> expression, Action<AutoFormComponentConfigurationBuilder<TType>> action)
        => AddMember(expression, action, AutoComponentDisplayType.FormCreate);
    
    public AutoFormMemberConfigurationBuilder AddMemberEditOnly<TType>(Expression<Func<TType>> expression, Action<AutoFormComponentConfigurationBuilder<TType>> action)
        => AddMember(expression, action, AutoComponentDisplayType.FormEdit);
    
    
    private AutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, Action<AutoFormComponentConfigurationBuilder<TType>> action, AutoComponentDisplayType displayType)
    {
        var property = expression.GetPropertyInfoForType(type);


        var formComponentConfigurations = configuration.ComponentConfigurations.GetValueOrDefault(displayType);

        formComponentConfigurations ??= [];
        var currentComponentConfig = formComponentConfigurations.FirstOrDefault(x => x.Property == property);
        
        if (currentComponentConfig != null)
        {
            if (!currentComponentConfig.AutoComponentTypes.ContainsKey(displayType))
            {
                currentComponentConfig.AutoComponentTypes.Add(displayType, type.GetDefaultAutoFormComponentForType());
            }
            
            var existentConfiguration = new AutoFormComponentConfigurationBuilder<TType>(currentComponentConfig);

            action(existentConfiguration);

            return this;
        }

        currentComponentConfig = new FormComponentConfiguration
        {
            Property = property,
            LabelName = $"Auto{displayType.ToString()}-LabelName-{type.Name}",
            AutoComponentTypes =
            {
                [displayType] = type.GetDefaultAutoFormComponentForType()
            }
        };
        formComponentConfigurations.Add(currentComponentConfig);
        configuration.ComponentConfigurations[displayType] = formComponentConfigurations;

        var autoFormComponentConfiguration = new AutoFormComponentConfigurationBuilder<TType>(currentComponentConfig);
        action(autoFormComponentConfiguration);

        return this;
    }
}