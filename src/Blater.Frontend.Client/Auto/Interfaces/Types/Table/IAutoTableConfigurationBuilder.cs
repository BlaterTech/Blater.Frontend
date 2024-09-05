using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;

namespace Blater.Frontend.Client.Auto.Interfaces.Types.Table;

public interface IAutoTableConfigurationBuilder
{
    IAutoTableConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoTableAutoComponentConfiguration componentConfiguration);
}