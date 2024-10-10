using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

using System.Linq.Expressions;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsPropertyConfigurationBuilder<TModel>
{
    IAutoDetailsTabsPropertyConfiguration<TModel> AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoDetailsTabsPropertyConfiguration<TModel, TPropertyType> propertyConfiguration);
}