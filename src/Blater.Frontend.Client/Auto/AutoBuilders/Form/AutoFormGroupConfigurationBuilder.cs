using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormGroupConfigurationBuilder<T>(FormGroupConfiguration<T> configuration) 
    : IAutoFormGroupConfigurationBuilder<T> 
    where T : BaseDataModel
{
    public IAutoFormPropertyConfigurationBuilder<T> AddPartner<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var current = new FormPropertyConfiguration<T>();
        configuration.PropertyConfigurations.Add(current);

        return new AutoFormPropertyConfigurationBuilder<T>(current);
    }
}