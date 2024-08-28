using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, FormGroupConfiguration configuration)
{
    public AutoFormPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression, AutoFormDisplayType formDisplayType = AutoFormDisplayType.All)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new FormPropertyConfiguration
        {
            AutoFormDisplayType = formDisplayType,
            Property = type.GetProperty(propertyName)!
        };
        
        configuration.Properties.Add(currentPropertyConfig);

        return new AutoFormPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
    }
}