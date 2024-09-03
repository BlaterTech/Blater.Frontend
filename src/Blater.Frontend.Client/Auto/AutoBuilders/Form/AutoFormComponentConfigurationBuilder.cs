using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Form;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormComponentConfigurationBuilder<TModel, TType>(AutoComponentDisplayType type, AutoFormComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder<TType>(configuration), IAutoFormComponentConfigurationBuilder<TModel, TType>
{
    public IAutoFormComponentConfigurationBuilder<TModel, TType> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TModel, TType> ComponentType(AutoFormComponentInputType value)
    {
        configuration.AutoComponentTypes[type] = value;
        return this;
    }
}