using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Form;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormComponentConfigurationBuilder<TType>(AutoFormAutoComponentConfiguration configuration)
    : AutoComponentConfigurationBuilder<TType>(configuration), 
      IAutoFormComponentConfigurationBuilder<TType>
{
    public IAutoFormComponentConfigurationBuilder<TType> LabelName(string value)
    {
        configuration.LabelName = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> Placeholder(string value)
    {
        configuration.Placeholder = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> HelpMessage(string value)
    {
        configuration.HelpMessage = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> IsReadOnly(bool value = false)
    {
        configuration.IsReadOnly = value;
        return this;
    }

    public IAutoFormComponentConfigurationBuilder<TType> ComponentType(AutoFormComponentInputType value)
    {
        configuration.AutoComponentType = value;
        return this;
    }
}