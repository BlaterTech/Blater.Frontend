using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormComponentConfigurationBuilder<TModel, TType>(AutoComponentDisplayType type, FormComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder<TType>(configuration), IAutoFormComponentConfigurationBuilder<TModel, TType>
{
    public IAutoFormComponentConfigurationBuilder<TModel, TType> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> ComponentType(AutoFormComponentInputType value)
    {
        configuration.AutoComponentTypes[type] = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> Validate(Action<IRuleBuilderInitial<TModel, TType>> action)
    {
        var parameter = Expression.Parameter(typeof(TModel), "x");
        var property = Expression.Property(parameter, configuration.Property);
        var expression = Expression.Lambda<Func<TModel, TType>>(property, parameter);

        var validator = new InlineValidator<TModel>();
        var rule = validator.RuleFor(expression);
        configuration.InlineValidator = validator;
        action(rule);
        
        return this;
    }
}