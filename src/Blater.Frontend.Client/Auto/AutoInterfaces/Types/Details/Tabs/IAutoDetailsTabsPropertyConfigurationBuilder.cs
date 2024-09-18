using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsPropertyConfigurationBuilder<TModel>
{
    IAutoDetailsTabsPropertyConfiguration<TModel> AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, IAutoDetailsTabsPropertyConfiguration<TModel> propertyConfiguration);
}