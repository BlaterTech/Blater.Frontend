using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.Interfaces.Types.Details;

public interface IAutoDetailsMemberConfigurationBuilder
{
    IAutoDetailsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsAutoComponentConfiguration componentConfiguration);
}