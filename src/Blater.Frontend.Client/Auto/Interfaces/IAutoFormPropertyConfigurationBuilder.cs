using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoFormComponentConfigurationBuilder<TModel, TType> : IAutoComponentConfigurationBuilder<TType>
{
    IAutoFormComponentConfigurationBuilder<TModel, TType> LabelName(string value);

    IAutoFormComponentConfigurationBuilder<TModel, TType> Placeholder(string value);

    IAutoFormComponentConfigurationBuilder<TModel, TType> HelpMessage(string value);

    IAutoFormComponentConfigurationBuilder<TModel, TType> IsReadOnly(bool value = false);

    IAutoFormComponentConfigurationBuilder<TModel, TType> ComponentType(AutoFormComponentInputType value);

    IAutoFormComponentConfigurationBuilder<TModel, TType> Validate(Action<IRuleBuilderInitial<TModel, TType>> action);
}