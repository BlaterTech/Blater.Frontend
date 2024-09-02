using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder<TModel>(Type type, FormGroupConfiguration configuration)
{
    public AutoFormMemberConfigurationBuilder<TModel> AddMember<TType>(Expression<Func<TType>> expression, Action<AutoFormComponentConfigurationBuilder<TModel, TType>> action)
        => AddMember(expression, AutoComponentDisplayType.Form, action);

    public AutoFormMemberConfigurationBuilder<TModel> AddMemberCreateOnly<TType>(Expression<Func<TType>> expression, Action<AutoFormComponentConfigurationBuilder<TModel, TType>> action)
        => AddMember(expression, AutoComponentDisplayType.FormCreate, action);

    public AutoFormMemberConfigurationBuilder<TModel> AddMemberEditOnly<TType>(Expression<Func<TType>> expression, Action<AutoFormComponentConfigurationBuilder<TModel, TType>> action)
        => AddMember(expression, AutoComponentDisplayType.FormEdit, action);


    private AutoFormMemberConfigurationBuilder<TModel> AddMember<TType>(Expression<Func<TType>> expression, AutoComponentDisplayType displayType, Action<AutoFormComponentConfigurationBuilder<TModel, TType>> action)
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
            
            var existentConfiguration = new AutoFormComponentConfigurationBuilder<TModel, TType>(displayType, currentComponentConfig);

            action(existentConfiguration);

            return this;
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

        var componentConfigurationBuilder = new AutoFormComponentConfigurationBuilder<TModel, TType>(displayType, currentComponentConfig);

        action(componentConfigurationBuilder);

        return this;
    }
}