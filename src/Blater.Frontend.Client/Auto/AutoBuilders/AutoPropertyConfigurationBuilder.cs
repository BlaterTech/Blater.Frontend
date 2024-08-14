using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoPropertyConfigurationBuilder<T>(BasePropertyConfiguration configuration) 
    : IAutoPropertyConfigurationBuilder<T> 
    where T : BaseDataModel
{
    public IAutoPropertyConfigurationBuilder<T> Breakpoint(Breakpoint breakpoint, int value)
    {
        configuration.Breakpoints.Add(breakpoint, value);
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T> DataFormat(string value)
    {
        configuration.DataFormat = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T> CssClass(string value)
    {
        configuration.CssClass = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T> CssStyle(string value)
    {
        configuration.CssStyle = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T> Order(int value)
    {
        configuration.Order = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T> LocalizationId(string value)
    {
        configuration.LocalizationId = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T> OnValueChanged<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T> OnClick<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T> OnParameterSet<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }
}