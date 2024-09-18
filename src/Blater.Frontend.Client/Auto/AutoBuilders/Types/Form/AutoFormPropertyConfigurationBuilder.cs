using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public class AutoFormPropertyConfigurationBuilder<TModel>(
    AutoComponentDisplayType displayType,
    AutoFormGroupConfiguration<TModel> configuration) :
    IAutoFormPropertyConfigurationBuilder<TModel>
{
    public AutoFormGroupConfiguration<TModel> AddSubgroup(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
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

    public IAutoFormPropertyConfiguration<TModel> AddMember<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                                                       IAutoFormPropertyConfiguration<TModel> propertyConfiguration)
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