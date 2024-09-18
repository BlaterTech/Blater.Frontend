using System.Linq.Expressions;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

public interface IAutoDetailsMemberConfigurationBuilder<TModel>
{
    IAutoDetailsPropertyConfiguration<TModel> AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, IAutoDetailsPropertyConfiguration<TModel> propertyConfiguration);
}