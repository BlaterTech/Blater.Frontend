using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

public interface IAutoDetailsMemberConfigurationBuilder
{
    IAutoDetailsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsAutoPropertyConfiguration propertyConfiguration);
}