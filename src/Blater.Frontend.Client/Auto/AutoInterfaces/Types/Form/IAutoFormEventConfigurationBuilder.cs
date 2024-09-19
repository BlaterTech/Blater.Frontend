using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormEventConfigurationBuilder<TModel, TPropertyType> : 
    IBaseAutoEventConfigurationBuilder<TPropertyType, AutoFormPropertyConfiguration<TModel, TPropertyType>>
{
}