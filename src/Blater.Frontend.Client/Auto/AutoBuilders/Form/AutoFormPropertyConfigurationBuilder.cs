using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Frontend.Client.Enumerations;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormPropertyConfigurationBuilder<T>(FormPropertyConfiguration<T> configuration) : IAutoFormPropertyConfigurationBuilder<T> where T : BaseDataModel
{
    public IAutoPropertyConfigurationBuilder<T, TProperty> AddPartner<TProperty>(Expression<Func<T, TProperty>> expression, AutoFormScope value = AutoFormScope.All)
    {
        configuration.FormScope = value;

        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        configuration.PropertyInfo = typeof(T).GetProperty(propertyName)!;
        
        return new AutoPropertyConfigurationBuilder<T, TProperty>(expression, configuration);
    }
}