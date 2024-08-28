using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormGroupConfigurationBuilder(Type type, FormGroupConfiguration configuration)
{
    public AutoFormPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression, AutoFormScope formScope = AutoFormScope.All)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new FormPropertyConfiguration
        {
            FormScope = formScope,
            PropertyInfo = type.GetProperty(propertyName)!
        };
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);

        return new AutoFormPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
    }
}