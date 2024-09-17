using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormMemberConfigurationBuilder<TModel>
{
    AutoFormGroupConfiguration AddSubgroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action);

    IAutoTablePropertyConfiguration AddMember<TProperty>(Expression<Func<TModel, TProperty>> expression,
                                                         IAutoTablePropertyConfiguration propertyConfiguration);
}