using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Enumerations;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormGroupConfigurationBuilder<T>(FormGroupConfiguration<T> configuration) where T : BaseDataModel
{
    public AutoFormPropertyConfigurationBuilder<T,TProperty> AddMember<TProperty>(Expression<Func<T, TProperty>> expression, AutoFormScope formScope = AutoFormScope.All)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new FormPropertyConfiguration<T>
        {
            FormScope = formScope,
            PropertyInfo = typeof(T).GetProperty(propertyName)!
        };
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);

        return new AutoFormPropertyConfigurationBuilder<T,TProperty>(expression, currentPropertyConfig);
    }
}