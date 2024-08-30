using System.Globalization;
using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoComponentConfigurationBuilder<TType>(BaseComponentConfiguration configuration) : IAutoComponentConfigurationBuilder<TType>
{
    public IAutoComponentConfigurationBuilder<TType> Breakpoint(Breakpoint breakpoint, int value)
    {
        configuration.Breakpoints.Add(breakpoint, value);
        return this;
    }

    public IAutoComponentConfigurationBuilder<TType> DataFormat(string value)
    {
        configuration.DataFormat = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder<TType> CssClass(string value)
    {
        configuration.ExtraClass = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder<TType> CssStyle(string value)
    {
        configuration.ExtraStyle = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder<TType> Order(int value)
    {
        configuration.Order = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder<TType> LocalizationId(string value)
    {
        configuration.LocalizationId = value;
        return this;
    }

    public IAutoComponentConfigurationBuilder<TType> OnValueChanged(Action<TType> action)
    {
        var genericMethod = EventCallback.Factory.Create<TType>(this, action.Invoke);
        
        configuration.ValueChanged[typeof(TType)] = "testando";
        
        return this;
    }

    public IAutoComponentConfigurationBuilder<TType> OnClick(EventCallback<TType> value)
    {
        throw new NotImplementedException();
    }

    public IAutoComponentConfigurationBuilder<TType> OnParameterSet(EventCallback<TType> value)
    {
        throw new NotImplementedException();
    }
}