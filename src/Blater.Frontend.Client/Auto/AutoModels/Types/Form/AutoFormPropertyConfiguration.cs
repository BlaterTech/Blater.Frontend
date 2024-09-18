using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormPropertyConfiguration<TModel, TPropertyValue> 
    : BaseAutoPropertyConfiguration<TPropertyValue>, IAutoFormPropertyConfiguration<TModel>
{
}