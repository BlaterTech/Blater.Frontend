using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoPropertyConfigurationBuilder
{
    IAutoPropertyConfigurationBuilder Breakpoint(Breakpoint breakpoint, int value);
    
    IAutoPropertyConfigurationBuilder DataFormat(string value);
    IAutoPropertyConfigurationBuilder CssClass(string value);
    IAutoPropertyConfigurationBuilder CssStyle(string value);
    IAutoPropertyConfigurationBuilder Order(int value);
    IAutoPropertyConfigurationBuilder LocalizationId(string value);

    IAutoPropertyConfigurationBuilder OnValueChanged<TProperty>(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder OnClick<TProperty>(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder OnParameterSet<TProperty>(EventCallback<TProperty> value);
}