using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableEventConfigurationBuilder<TPropertyType> : IBaseAutoEventConfigurationBuilder<TPropertyType, AutoTablePropertyConfiguration<TPropertyType>>
{
}