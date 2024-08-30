using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoFormComponentConfigurationBuilder<TType> : IAutoComponentConfigurationBuilder<TType>
{
    IAutoFormComponentConfigurationBuilder<TType> LabelName(string value);

    IAutoFormComponentConfigurationBuilder<TType> Placeholder(string value);

    IAutoFormComponentConfigurationBuilder<TType> HelpMessage(string value);

    IAutoFormComponentConfigurationBuilder<TType> IsReadOnly(bool value = false);

    IAutoFormComponentConfigurationBuilder<TType> ComponentType(AutoFormComponentInputType value);

    IAutoFormComponentConfigurationBuilder<TType> Validate(Action<IRuleBuilderInitial<object, TType>> action);
}