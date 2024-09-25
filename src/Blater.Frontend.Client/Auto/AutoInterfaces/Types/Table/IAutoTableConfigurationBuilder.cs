using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;
using Blater.Frontend.Client.Contracts;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableConfigurationBuilder<TModel>
{
    CustomAutoTableAction AddCustomAction(CustomAutoTableAction autoTableAction);
    AutoTablePagerConfiguration ModifyTablePager(AutoTablePagerConfiguration pagerConfiguration);
    AutoTablePropertyConfiguration<TModel, TPropertyType> AddMemberOnly<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration);
    IAutoTableEventConfigurationBuilder<TModel, TPropertyType> AddMemberWithEvent<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration);
}