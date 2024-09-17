using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Base;

public interface IBaseAutoEventConfiguration<TPropertyType>
{
    public EventCallback<TPropertyType> OnValueChanged { get; set; }   
    public EventCallback<EventArgs> OnClick { get; set; }   
}