using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoFormComponentConfigurationBuilder<out TProperty> : IAutoComponentConfigurationBuilder
{
    IAutoFormComponentConfigurationBuilder<TProperty> LabelName(string value);

    IAutoFormComponentConfigurationBuilder<TProperty> Placeholder(string value);

    IAutoFormComponentConfigurationBuilder<TProperty> HelpMessage(string value);

    IAutoFormComponentConfigurationBuilder<TProperty> IsReadOnly(bool value = false);

    IAutoFormComponentConfigurationBuilder<TProperty> ComponentType(AutoFormComponentInputType value);

    IAutoFormComponentConfigurationBuilder<TProperty> Validate(Action<IRuleBuilderInitial<object, TProperty>> action);
}