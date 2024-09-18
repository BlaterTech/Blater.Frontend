using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

public interface IAutoDetailsMemberConfigurationBuilder<TModel>
{
    IAutoDetailsPropertyConfiguration<TModel> AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoDetailsPropertyConfiguration<TModel, TPropertyType> propertyConfiguration);
}