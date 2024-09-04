using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseAutoEventConfiguration
{
    public object? OnValueChanged { get; set; }
    public object? OnParameterSet { get; set; }
    public EventCallback<MouseEventArgs> OnClick { get; set; }
}