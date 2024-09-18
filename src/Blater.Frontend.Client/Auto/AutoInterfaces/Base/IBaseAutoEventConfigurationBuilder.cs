using Blater.Frontend.Client.Auto.AutoModels.Base;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Base;

public interface IBaseAutoEventConfigurationBuilder<out TPropertyType, out TResponseType> 
    where TResponseType : BaseAutoPropertyConfiguration<TPropertyType>
{
    TResponseType AddOnValueChanged(Action<TPropertyType> onValueChanged);
    TResponseType AddOnClick(EventCallback<EventArgs> onClick);
}