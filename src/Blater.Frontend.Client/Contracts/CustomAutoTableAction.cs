using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using MudBlazor;

namespace Blater.Frontend.Client.Contracts;

public class CustomAutoTableAction
{
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    public Color Color { get; set; } = Color.Primary;

    public required string Icon { get; set; }

    public string? Tooltip { get; set; }
}