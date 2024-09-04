using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.Auto.Interfaces.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, AutoFormGroupConfiguration configuration) : IAutoFormMemberConfigurationBuilder
{
    #region Members

    public IAutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
        => AddMember(expression, AutoComponentDisplayType.Form, componentConfiguration);

    public IAutoFormMemberConfigurationBuilder AddMemberCreateOnly<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
        => AddMember(expression, AutoComponentDisplayType.FormCreate, componentConfiguration);

    public IAutoFormMemberConfigurationBuilder AddMemberEditOnly<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
        => AddMember(expression, AutoComponentDisplayType.FormEdit, componentConfiguration);

    #endregion

    private AutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoComponentDisplayType displayType,
                                                                AutoFormAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);

        AutoFormAutoComponentConfiguration? currentComponentConfig;
        if (configuration.ComponentConfigurations.TryGetValue(displayType, out var value))
        {
            currentComponentConfig = value.FirstOrDefault(x => x.Property == property);
            if (currentComponentConfig != null)
            {
                currentComponentConfig = componentConfiguration;

                if (currentComponentConfig.AutoComponentType == null)
                {
                    currentComponentConfig.AutoComponentType = property.GetDefaultAutoFormComponentForType();
                }

                return this;
            }
        }

        currentComponentConfig = componentConfiguration;
        currentComponentConfig.Property = property;
        currentComponentConfig.AutoComponentType ??= property.GetDefaultAutoFormComponentForType();
        value ??= [];
        value.Add(currentComponentConfig);
        configuration.ComponentConfigurations.TryAdd(displayType, value);

        return this;
    }
}