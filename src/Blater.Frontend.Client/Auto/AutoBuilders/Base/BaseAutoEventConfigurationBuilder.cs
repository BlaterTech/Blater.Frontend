using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Base;

public class BaseAutoEventConfigurationBuilder<TPropertyType, TResponseType>(BaseAutoPropertyConfiguration<TPropertyType> configuration)
    : IBaseAutoEventConfigurationBuilder<TPropertyType, TResponseType> 
    where TResponseType : BaseAutoPropertyConfiguration<TPropertyType>
{
    public TResponseType AddOnValueChanged(Action<TPropertyType> onValueChanged)
    {
        configuration.OnValueChanged = EventCallback.Factory.Create(this, onValueChanged);
        return (TResponseType)configuration;
    }

    public TResponseType AddOnClick(Action onClick)
    {
        configuration.OnClick = EventCallback.Factory.Create<EventArgs>(this, onClick);
        return (TResponseType)configuration;
    }
}