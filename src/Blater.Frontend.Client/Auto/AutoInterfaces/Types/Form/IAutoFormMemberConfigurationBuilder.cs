using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormPropertyConfigurationBuilder<TModel>
{
    AutoFormGroupConfiguration<TModel> AddSubgroup(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);

    IAutoFormPropertyConfiguration<TModel> AddMember<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                                                AutoFormPropertyConfiguration<TModel, TProperty> propertyConfiguration);
}