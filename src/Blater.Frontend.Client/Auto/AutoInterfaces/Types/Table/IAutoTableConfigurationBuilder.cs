using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableConfigurationBuilder<TModel>
{
    AutoTablePropertyConfiguration<TPropertyType> AddMemberOnly<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TPropertyType> propertyConfiguration);
    IAutoTableEventConfigurationBuilder<TPropertyType> AddMemberWithEvent<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TPropertyType> propertyConfiguration);
}