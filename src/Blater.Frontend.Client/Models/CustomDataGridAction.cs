using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Blater.Frontend.Client.Models;

public class CustomAutoTableAction<TModel>
{
    public EventCallback<MouseEventArgs> OnValueChanged { get; }
    
    public Color? Color { get; }
    
    public string Icon { get; } = null!;
    
    public string Tooltip { get; set; } = null!;
}