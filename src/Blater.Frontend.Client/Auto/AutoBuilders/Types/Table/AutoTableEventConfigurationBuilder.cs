using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;

public class AutoTableEventConfigurationBuilder<TModel, TPropertyType>(
    AutoTablePropertyConfiguration<TModel, TPropertyType> configuration) 
    : BaseAutoEventConfigurationBuilder<TModel, TPropertyType, AutoTablePropertyConfiguration<TModel, TPropertyType>>(configuration), 
      IAutoTableEventConfigurationBuilder<TModel, TPropertyType>
{
}