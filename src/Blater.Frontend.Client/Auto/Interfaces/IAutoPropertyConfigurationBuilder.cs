using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoPropertyConfigurationBuilder<T, TProperty> where T : BaseDataModel
{
    IAutoPropertyConfigurationBuilder<T, TProperty> DataFormat(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> CssClass(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> CssStyle(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> Order(int value);   
    IAutoPropertyConfigurationBuilder<T, TProperty> LocalizationId(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> ComponentType(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> Disable(bool value = false);
    
    IAutoPropertyConfigurationBuilder<T, TProperty> OnValueChanged(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder<T, TProperty> OnClick(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder<T, TProperty> OnParameterSet(EventCallback<TProperty> value);
}