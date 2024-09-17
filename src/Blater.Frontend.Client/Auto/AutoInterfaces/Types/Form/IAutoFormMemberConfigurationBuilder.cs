using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormMemberConfigurationBuilder<TModel>
{
    //IAutoFormMemberConfigurationBuilder AddSubgroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action);
    AutoFormAutoPropertyConfiguration<TProperty> AddMember<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                                                      AutoFormAutoPropertyConfiguration<TProperty> propertyConfiguration);
}