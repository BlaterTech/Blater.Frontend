namespace Blater.Frontend.Client.Auto.AutoInterfaces.Base;

public interface IBaseAutoEventConfigurationBuilder<out TPropertyType, out TResponseType> 
    where TResponseType : IBaseAutoPropertyConfiguration
{
    TResponseType AddOnValueChanged(Action<TPropertyType> onValueChanged);
    TResponseType AddOnClick(Action onClick);
}