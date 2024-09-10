using Blater.Models.Bases;
using MudBlazor;

namespace Blater.Frontend.Client.Models;

public class CustomAutoTableAction
{
    public object? OnValueChanged { get; set; }
    
    public Color? Color { get; set; }
    
    public string Icon { get; set; } = null!;
    
    public string Tooltip { get; set; } = null!;
}