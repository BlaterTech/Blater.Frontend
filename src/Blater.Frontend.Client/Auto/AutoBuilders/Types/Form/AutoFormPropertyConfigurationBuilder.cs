﻿using System.Linq.Expressions;
using Blater.Extensions;
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

    public AutoFormPropertyConfiguration<TModel, TProperty> AddMemberOnly<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                                                                     AutoFormPropertyConfiguration<TModel, TProperty> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return propertyConfiguration;
    }
    
    public IAutoFormEventConfigurationBuilder<TModel, TProperty> AddMemberWithEvent<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                                                                               AutoFormPropertyConfiguration<TModel, TProperty> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return new AutoFormEventConfigurationBuilder<TModel, TProperty>(propertyConfiguration);
    }

    private void AddMember<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                      AutoFormPropertyConfiguration<TModel, TProperty> propertyConfiguration)
    {
        var propertyInfo = expression.GetPropertyInfo();

        var index = configuration.ComponentConfigurations.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            configuration.ComponentConfigurations[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = propertyInfo;
            propertyConfiguration.LabelNameLocalizationId ??= $"Blater-AutoForm-{typeof(TModel).Name}-Member-LabelName-{propertyInfo.Name}";
            propertyConfiguration.PlaceHolderLocalizationId ??= $"Blater-AutoForm-{typeof(TModel).Name}-Member-PlaceHolder-{propertyInfo.Name}";
            propertyConfiguration.Placeholder ??= propertyInfo.Name;
            propertyConfiguration.LabelName ??= propertyInfo.Name;
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultComponentForForm();
            configuration.ComponentConfigurations.Add(propertyConfiguration);
        }
    }
}