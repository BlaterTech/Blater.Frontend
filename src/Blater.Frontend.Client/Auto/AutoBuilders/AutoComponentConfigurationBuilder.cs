using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoComponentConfigurationBuilder(BaseComponentConfiguration configuration) : IAutoComponentConfigurationBuilder
{
    public IAutoComponentConfigurationBuilder Breakpoint(Breakpoint breakpoint, int value)
    {
        configuration.Breakpoints.Add(breakpoint, value);
        return this;
    }

    public IAutoComponentConfigurationBuilder DataFormat(string value)
    {
        configuration.DataFormat = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder CssClass(string value)
    {
        configuration.ExtraClass = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder CssStyle(string value)
    {
        configuration.ExtraStyle = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder Order(int value)
    {
        configuration.Order = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder LocalizationId(string value)
    {
        configuration.LocalizationId = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder OnValueChanged<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoComponentConfigurationBuilder OnClick<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoComponentConfigurationBuilder OnParameterSet<TProperty>(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }
}