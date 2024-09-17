using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public class AutoFormPropertyConfigurationBuilder<TModel>(
    AutoComponentDisplayType displayType,
    AutoFormGroupConfiguration configuration) :
    IAutoFormMemberConfigurationBuilder<TModel>
{
    public AutoFormGroupConfiguration AddSubgroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
    {
        if (!configuration.SubGroups.TryGetValue(displayType, out var value))
        {
            value ??= [];
            configuration.SubGroups.TryAdd(displayType, value);
        }

        var index = value.IndexOf(groupConfiguration);
        if (index != -1)
        {
            value[index] = groupConfiguration;
        }
        else
        {
            value.Add(groupConfiguration);
        }

        configuration.SubGroups[displayType] = value;

        var builder = new AutoFormPropertyConfigurationBuilder<TModel>(displayType, groupConfiguration);

        action.Invoke(builder);

        return groupConfiguration;
    }

    public IAutoTablePropertyConfiguration AddMember<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                                                IAutoTablePropertyConfiguration propertyConfiguration)
    {
        var modelType = typeof(TModel);
        var propType = typeof(TProperty);
        var propertyInfo = modelType.GetProperty(propType.Name);
        if (propertyInfo == null)
        {
            throw new Exception($"Property {propType.Name} not found in {modelType.Name}");
        }

        var index = configuration.ComponentConfigurations.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            configuration.ComponentConfigurations[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = propertyInfo;
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultAutoFormComponentForType();
            configuration.ComponentConfigurations.Add(propertyConfiguration);
        }

        return propertyConfiguration;
    }
}