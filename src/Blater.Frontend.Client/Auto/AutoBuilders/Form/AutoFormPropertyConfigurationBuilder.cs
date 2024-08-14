using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormPropertyConfigurationBuilder<T, TProperty> : IAutoFormPropertyConfigurationBuilder<T, TProperty> where T : BaseDataModel
{
    private readonly Expression<Func<T, TProperty>> _expression;

    public AutoFormPropertyConfigurationBuilder(Expression<Func<T, TProperty>> expression)
    {
        _expression = expression;
    }
    
    public IAutoFormPropertyConfigurationBuilder<T, TProperty> LabelName(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoFormPropertyConfigurationBuilder<T, TProperty> Placeholder(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoFormPropertyConfigurationBuilder<T, TProperty> HelpMessage(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoFormGroupConfigurationBuilder<T, TProperty> Breakpoint(Breakpoint breakpoint, int value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Name(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> DataFormat(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> CssClass(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> CssStyle(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Order(int value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> LocalizationId(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> ComponentType(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Disable(bool value = false)
    {
        throw new NotImplementedException();
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
}