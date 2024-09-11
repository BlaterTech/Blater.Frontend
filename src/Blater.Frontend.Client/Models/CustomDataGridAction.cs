using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Blater.Frontend.Client.Models;

public class CustomAutoTableAction
{
    public EventCallback<MouseEventArgs> OnValueChanged { get; set; }
    
    public Color? Color { get; set; }
    
    public string Icon { get; set; } = null!;
    
    public string Tooltip { get; set; } = null!;
}