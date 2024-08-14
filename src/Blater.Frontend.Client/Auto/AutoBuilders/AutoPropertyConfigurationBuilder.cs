using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Models.Bases;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoPropertyConfigurationBuilder<T, TProperty>(Expression<Func<T, TProperty>> expression, BasePropertyConfiguration<T> configuration) 
    : IAutoPropertyConfigurationBuilder<T, TProperty> 
    where T : BaseDataModel
{
    public IAutoPropertyConfigurationBuilder<T, TProperty> Breakpoint(Breakpoint breakpoint, int value)
    {
        configuration.Breakpoints.Add(breakpoint, value);
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> DataFormat(string value)
    {
        configuration.DataFormat = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> CssClass(string value)
    {
        configuration.CssClass = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> CssStyle(string value)
    {
        configuration.CssStyle = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Order(int value)
    {
        configuration.Order = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> LocalizationId(string value)
    {
        configuration.LocalizationId = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> ComponentType(string value)
    {
        configuration.ComponentType = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Disable(bool value = false)
    {
        configuration.Disable = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> OnValueChanged(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> OnClick(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> OnParameterSet(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Validate(Action<IRuleBuilderInitial<T, TProperty>> action)
    {
        var validator = configuration.Validator ??= new InlineValidator<T>();
        var rule = validator.RuleFor(expression);
        action(rule);
        
        return this;
    }
}