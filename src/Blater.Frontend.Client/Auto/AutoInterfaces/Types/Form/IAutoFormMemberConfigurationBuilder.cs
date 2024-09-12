using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormMemberConfigurationBuilder
{
    IAutoFormMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration);
}