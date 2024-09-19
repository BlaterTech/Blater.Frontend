namespace Blater.Frontend.Client.Auto.AutoInterfaces.Base;

public interface IBaseAutoEventConfigurationBuilder<TPropertyType, out TResponseType>
    where TResponseType : IBaseAutoPropertyConfigurationValue<TPropertyType>
{
    TResponseType AddOnValueChanged(Func<TPropertyType, TPropertyType> onValueChanged);
    TResponseType AddOnValueChanged(Action<TPropertyType> onValueChanged, IBaseAutoPropertyConfigurationValue<TPropertyType> value);
    TResponseType AddOnValueChanged(Action<TPropertyType> onValueChanged, params IBaseAutoPropertyConfigurationValue<TPropertyType>[] values);
    TResponseType AddOnClick(Action onClick);
}