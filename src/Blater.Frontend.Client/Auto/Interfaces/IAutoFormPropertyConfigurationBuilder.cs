using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoFormPropertyConfigurationBuilder<out TProperty> : IAutoPropertyConfigurationBuilder
{
    IAutoFormPropertyConfigurationBuilder<TProperty> LabelName(string value);

    IAutoFormPropertyConfigurationBuilder<TProperty> Placeholder(string value);

    IAutoFormPropertyConfigurationBuilder<TProperty> HelpMessage(string value);

    IAutoFormPropertyConfigurationBuilder<TProperty> IsReadOnly(bool value = false);

    IAutoFormPropertyConfigurationBuilder<TProperty> ComponentType(AutoFormComponentInputType value);

    IAutoFormPropertyConfigurationBuilder<TProperty> Validate(Action<IRuleBuilderInitial<object, TProperty>> action);
}