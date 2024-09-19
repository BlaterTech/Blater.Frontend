using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public class AutoFormEventConfigurationBuilder<TModel, TPropertyType>(
    AutoFormPropertyConfiguration<TModel, TPropertyType> configuration) : 
    BaseAutoEventConfigurationBuilder<TModel, TPropertyType, AutoFormPropertyConfiguration<TModel, TPropertyType>>(configuration), 
    IAutoFormEventConfigurationBuilder<TModel, TPropertyType>
{
}