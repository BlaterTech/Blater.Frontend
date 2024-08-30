using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, FormGroupConfiguration configuration)
{
    public AutoFormComponentConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        AddMember(expression, AutoComponentDisplayType.FormCreate);
        return AddMember(expression, AutoComponentDisplayType.FormEdit);
    }
    
    public AutoFormComponentConfigurationBuilder<TProperty> AddMemberCreateOnly<TProperty>(Expression<Func<TProperty>> expression)
        => AddMember(expression, AutoComponentDisplayType.FormCreate);
    
    public AutoFormComponentConfigurationBuilder<TProperty> AddMemberEditOnly<TProperty>(Expression<Func<TProperty>> expression)
        => AddMember(expression, AutoComponentDisplayType.FormEdit);
    
    
    private AutoFormComponentConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression, AutoComponentDisplayType displayType)
    {
        var property = expression.GetPropertyInfoForType(type);


        var formComponentConfigurations = configuration.ComponentConfigurations.GetValueOrDefault(displayType);

        FormComponentConfiguration? currentComponentConfig;
        formComponentConfigurations ??= [];
        currentComponentConfig = formComponentConfigurations.FirstOrDefault(x => x.Property == property);
        
        if (currentComponentConfig != null)
        {
            if (!currentComponentConfig.AutoComponentTypes.ContainsKey(displayType))
            {
                currentComponentConfig.AutoComponentTypes.Add(displayType, type.GetDefaultAutoFormComponentForType());
            }
            
            return new AutoFormComponentConfigurationBuilder<TProperty>(currentComponentConfig);
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

        return new AutoFormComponentConfigurationBuilder<TProperty>(currentComponentConfig);
    }
}