using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsMemberConfigurationBuilder
{
    IAutoDetailsTabsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsTabsComponentConfiguration componentConfiguration);
}