namespace Blater.Frontend.Client.Auto.AutoInterfaces.Base;

public interface IBaseAutoPropertyConfigurationValue<TPropertyValue>
{
    TPropertyValue? Value { get; set; }
}