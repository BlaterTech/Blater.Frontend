using Blater.Models.Bases;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoPropertyConfigurationBuilder<T, TProperty>
    where T : BaseDataModel
{
    IAutoPropertyConfigurationBuilder<T, TProperty> Breakpoint(Breakpoint breakpoint, int value);
    IAutoPropertyConfigurationBuilder<T, TProperty> Name(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> LabelName(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> Placeholder(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> HelpMessage(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> IsReadOnly(bool value = false);
    IAutoPropertyConfigurationBuilder<T, TProperty> DataFormat(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> CssClass(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> CssStyle(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> Order(int value);
    IAutoPropertyConfigurationBuilder<T, TProperty> LocalizationId(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> ComponentType(string value);
    IAutoPropertyConfigurationBuilder<T, TProperty> Disable(bool value = false);
    IAutoPropertyConfigurationBuilder<T, TProperty> DisableFilter(bool value = false);
    IAutoPropertyConfigurationBuilder<T, TProperty> DisableSortBy(bool value = false);

    IAutoPropertyConfigurationBuilder<T, TProperty> OnValueChanged(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder<T, TProperty> OnClick(EventCallback<TProperty> value);
    IAutoPropertyConfigurationBuilder<T, TProperty> OnParameterSet(EventCallback<TProperty> value);

    IAutoPropertyConfigurationBuilder<T, TProperty> Validate(Action<IRuleBuilderInitial<T, TProperty>> action);
}