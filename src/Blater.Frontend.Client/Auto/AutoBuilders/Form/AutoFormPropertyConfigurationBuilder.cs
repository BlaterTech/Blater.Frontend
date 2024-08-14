using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormPropertyConfigurationBuilder<T, TProperty>(Expression<Func<T, TProperty>> expression, FormPropertyConfiguration<T> configuration)
    : AutoPropertyConfigurationBuilder<T>(configuration)
    where T : BaseDataModel
{
    public AutoFormPropertyConfigurationBuilder<T, TProperty> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<T, TProperty> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<T, TProperty> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<T, TProperty> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<T, TProperty> ComponentType(string value)
    {
        configuration.ComponentType = value;
        return this;
    }

    public AutoFormPropertyConfigurationBuilder<T, TProperty> Validate(Action<IRuleBuilderInitial<T, TProperty>> action)
    {
        var validator = configuration.Validator ??= new InlineValidator<T>();
        var rule = validator.RuleFor(expression);
        action(rule);
        
        return this;
    }
}