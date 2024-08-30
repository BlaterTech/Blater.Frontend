using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormComponentConfigurationBuilder<TProperty>(FormComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder(configuration), IAutoFormComponentConfigurationBuilder<TProperty>
{
    public IAutoFormComponentConfigurationBuilder<TProperty> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TProperty> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TProperty> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TProperty> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TProperty> ComponentType(AutoFormComponentInputType value)
    {
        configuration.AutoComponentTypes[AutoComponentDisplayType.Form] = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TProperty> Validate(Action<IRuleBuilderInitial<object, TProperty>> action)
    {
        var parameter = Expression.Parameter(typeof(object), "x");
        var property = Expression.Property(parameter, configuration.Property);
        var expression = Expression.Lambda<Func<object, TProperty>>(property, parameter);
        
        var validator = configuration.Validator ??= new InlineValidator<object>();
        var rule = validator.RuleFor(expression);
        action(rule);
        
        return this;
    }
}