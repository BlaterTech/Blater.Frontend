using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoComponentConfigurationBuilder
{
    IAutoComponentConfigurationBuilder Breakpoint(Breakpoint breakpoint, int value);
    
    IAutoComponentConfigurationBuilder DataFormat(string value);
    IAutoComponentConfigurationBuilder CssClass(string value);
    IAutoComponentConfigurationBuilder CssStyle(string value);
    IAutoComponentConfigurationBuilder Order(int value);
    IAutoComponentConfigurationBuilder LocalizationId(string value);

    IAutoComponentConfigurationBuilder OnValueChanged<TProperty>(EventCallback<TProperty> value);
    IAutoComponentConfigurationBuilder OnClick<TProperty>(EventCallback<TProperty> value);
    IAutoComponentConfigurationBuilder OnParameterSet<TProperty>(EventCallback<TProperty> value);
}