using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoEventComponentConfiguration<TType>
{
    public EventCallback<TType> ValueChanged { get; set; }
    public EventCallback<TType> OnParameterSet { get; set; }
    public EventCallback<TType> OnClick { get; set; }
}