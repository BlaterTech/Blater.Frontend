namespace Blater.Frontend.Client.Auto.AutoInterfaces.Base;

public interface IBaseAutoPropertyConfigurationValue<TPropertyValue> :
    IBaseAutoPropertyConfiguration,
    IBaseAutoEventConfiguration<TPropertyValue>
{
    TPropertyValue? Value { get; set; }
}