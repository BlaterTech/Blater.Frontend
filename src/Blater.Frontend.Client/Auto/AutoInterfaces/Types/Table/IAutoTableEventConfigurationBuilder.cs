using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableEventConfigurationBuilder<TModel, TPropertyType> : IBaseAutoEventConfigurationBuilder<TPropertyType, AutoTablePropertyConfiguration<TModel, TPropertyType>>
{
}