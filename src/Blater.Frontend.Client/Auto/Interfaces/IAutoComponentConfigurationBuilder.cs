using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoComponentConfigurationBuilder<out TType>
{
    IAutoComponentConfigurationBuilder<TType> Breakpoint(Breakpoint breakpoint, int value);
    IAutoComponentConfigurationBuilder<TType> DataFormat(string value);
    IAutoComponentConfigurationBuilder<TType> CssClass(string value);
    IAutoComponentConfigurationBuilder<TType> CssStyle(string value);
    IAutoComponentConfigurationBuilder<TType> Order(int value);
    IAutoComponentConfigurationBuilder<TType> LocalizationId(string value);
    IAutoComponentConfigurationBuilder<TType> OnValueChanged(Action<TType> action);
    IAutoComponentConfigurationBuilder<TType> OnClick(Action<TType> value);
    IAutoComponentConfigurationBuilder<TType> OnParameterSet(Action<TType> value);
}