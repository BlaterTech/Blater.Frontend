using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormGroupConfigurationBuilder<T>(FormGroupConfiguration<T> configuration) where T : BaseDataModel
{
    public AutoFormMemberConfigurationBuilder<T> AddMember<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var current = new FormPropertyConfiguration<T>();
        configuration.PropertyConfigurations.Add(current);

        return new AutoFormMemberConfigurationBuilder<T>(current);
    }
}