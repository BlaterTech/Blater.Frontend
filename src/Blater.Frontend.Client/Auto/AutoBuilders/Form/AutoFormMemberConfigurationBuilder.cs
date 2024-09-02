using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, FormGroupConfiguration configuration)
{
    public IAutoFormComponentConfigurationBuilder<TType> AddMember<TType>(Expression<Func<TType>> expression)
        => AddMember(expression, AutoComponentDisplayType.Form);

    public IAutoFormComponentConfigurationBuilder<TType> AddMemberCreateOnly<TType>(Expression<Func<TType>> expression)
        => AddMember(expression, AutoComponentDisplayType.FormCreate);

    public IAutoFormComponentConfigurationBuilder<TType> AddMemberEditOnly<TType>(Expression<Func<TType>> expression)
        => AddMember(expression, AutoComponentDisplayType.FormEdit);


    private AutoFormComponentConfigurationBuilder<TType> AddMember<TType>(Expression<Func<TType>> expression, AutoComponentDisplayType displayType)
    {
        var property = expression.GetPropertyInfoForType(type);

        var formComponentConfigurations = configuration.ComponentConfigurations.GetValueOrDefault(displayType);

        formComponentConfigurations ??= [];
        var currentComponentConfig = formComponentConfigurations.FirstOrDefault(x => x.Property == property);

        if (currentComponentConfig != null)
        {
            if (!currentComponentConfig.AutoComponentTypes.ContainsKey(displayType))
            {
                currentComponentConfig.AutoComponentTypes.Add(displayType, property.GetDefaultAutoFormComponentForType());
            }

            return new AutoFormComponentConfigurationBuilder<TType>(displayType, currentComponentConfig);
        }

        currentComponentConfig = new FormComponentConfiguration
        {
            Property = property,
            LabelName = $"Auto{displayType.ToString()}-LabelName-{type.Name}",
            AutoComponentTypes =
            {
                [displayType] = property.GetDefaultAutoFormComponentForType()
            }
        };
        formComponentConfigurations.Add(currentComponentConfig);
        configuration.ComponentConfigurations[displayType] = formComponentConfigurations;

        return new AutoFormComponentConfigurationBuilder<TType>(displayType, currentComponentConfig);
    }
}