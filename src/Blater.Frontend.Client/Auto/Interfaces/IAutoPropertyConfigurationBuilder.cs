using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoPropertyConfigurationBuilder<T>
    where T : BaseDataModel
{
    IAutoPropertyConfigurationBuilder<T> Breakpoint(Breakpoint breakpoint, int value);
    
    IAutoPropertyConfigurationBuilder<T> DataFormat(string value);
    IAutoPropertyConfigurationBuilder<T> CssClass(string value);
    IAutoPropertyConfigurationBuilder<T> CssStyle(string value);
    IAutoPropertyConfigurationBuilder<T> Order(int value);
    IAutoPropertyConfigurationBuilder<T> LocalizationId(string value);

    IAutoPropertyConfigurationBuilder<T> OnValueChanged<TProperty>(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder<T> OnClick<TProperty>(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder<T> OnParameterSet<TProperty>(EventCallback<TProperty> value);
}