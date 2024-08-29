using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, FormGroupConfiguration configuration)
{
    public AutoFormPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        AddMember(expression, AutoComponentDisplayType.FormCreate);
        return AddMember(expression, AutoComponentDisplayType.FormEdit);
    }
    
    public AutoFormPropertyConfigurationBuilder<TProperty> AddMemberCreateOnly<TProperty>(Expression<Func<TProperty>> expression)
        => AddMember(expression, AutoComponentDisplayType.FormCreate);
    
    public AutoFormPropertyConfigurationBuilder<TProperty> AddMemberEditOnly<TProperty>(Expression<Func<TProperty>> expression)
        => AddMember(expression, AutoComponentDisplayType.FormEdit);
    
    
    private AutoFormPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression, AutoComponentDisplayType displayType)
    {
        var property = expression.GetPropertyInfoForType(type);

        var currentPropertyConfig = configuration.Properties
                                                 .Select(x =>
                                                             x.Value.FirstOrDefault(c => c.Property == property))
                                                 .FirstOrDefault();

        if (currentPropertyConfig != null)
        {
            if (!currentPropertyConfig.AutoComponentTypes.ContainsKey(displayType))
            {
                currentPropertyConfig.AutoComponentTypes.Add(displayType, type.GetDefaultAutoFormComponentForType());
            }
            
            return new AutoFormPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
        }

        currentPropertyConfig = new FormPropertyConfiguration
        {
            Property = property,
            LabelName = $"Auto{displayType.ToString()}-LabelName-{type.Name}",
            AutoComponentTypes =
            {
                [displayType] = type.GetDefaultAutoFormComponentForType()
            }
        };

        configuration.Properties[displayType].Add(currentPropertyConfig);

        return new AutoFormPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
    }
}