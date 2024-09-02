using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormComponentConfigurationBuilder<TType>(AutoComponentDisplayType type, FormComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder<TType>(configuration), IAutoFormComponentConfigurationBuilder<TType>
{
    public IAutoFormComponentConfigurationBuilder<TType> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> ComponentType(AutoFormComponentInputType value)
    {
        configuration.AutoComponentTypes[type] = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> Validate(Action<IRuleBuilderInitial<object, TType>> action)
    {
        var parameter = Expression.Parameter(typeof(object), "x");
        var property = Expression.Property(parameter, configuration.Property);
        var expression = Expression.Lambda<Func<object, TType>>(property, parameter);
        
        var validator = configuration.Validator ??= new InlineValidator<object>();
        var rule = validator.RuleFor(expression);
        action(rule);
        
        return this;
    }
}