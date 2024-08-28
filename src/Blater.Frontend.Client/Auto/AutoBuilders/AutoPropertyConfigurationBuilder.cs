using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoPropertyConfigurationBuilder(BasePropertyConfiguration configuration) : IAutoPropertyConfigurationBuilder
{
    public IAutoPropertyConfigurationBuilder Breakpoint(Breakpoint breakpoint, int value)
    {
        configuration.Breakpoints.Add(breakpoint, value);
        return this;
    }

    public IAutoPropertyConfigurationBuilder DataFormat(string value)
    {
        configuration.DataFormat = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder CssClass(string value)
    {
        configuration.CssClass = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder CssStyle(string value)
    {
        configuration.CssStyle = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder Order(int value)
    {
        configuration.Order = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder LocalizationId(string value)
    {
        configuration.LocalizationId = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder OnValueChanged<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder OnClick<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder OnParameterSet<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }
}