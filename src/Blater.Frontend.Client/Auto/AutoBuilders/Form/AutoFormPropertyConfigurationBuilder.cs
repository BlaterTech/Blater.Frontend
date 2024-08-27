using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormPropertyConfigurationBuilder<TProperty>(FormPropertyConfiguration configuration)
    : AutoPropertyConfigurationBuilder(configuration)
{
    public AutoFormPropertyConfigurationBuilder<TProperty> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<TProperty> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<TProperty> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<TProperty> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<TProperty> ComponentType(string value)
    {
        configuration.ComponentType = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<TProperty> Validate(Action<IRuleBuilderInitial<object, TProperty>> action)
    {
        var parameter = Expression.Parameter(typeof(object), "x");
        var property = Expression.Property(parameter, configuration.PropertyInfo);
        var expression = Expression.Lambda<Func<object, TProperty>>(property, parameter);
        
        var validator = configuration.Validator ??= new InlineValidator<object>();
        var rule = validator.RuleFor(expression);
        action(rule);
        
        return this;
    }
}