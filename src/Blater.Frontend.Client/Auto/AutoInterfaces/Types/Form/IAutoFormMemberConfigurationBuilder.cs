using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormMemberConfigurationBuilder
{
    IAutoFormMemberConfigurationBuilder AddSubgroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action);
    IAutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration);
}