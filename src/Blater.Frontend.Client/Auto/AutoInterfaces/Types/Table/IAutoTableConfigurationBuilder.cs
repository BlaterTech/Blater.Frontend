using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableConfigurationBuilder
{
    IAutoTableConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoTableAutoComponentConfiguration componentConfiguration);
}