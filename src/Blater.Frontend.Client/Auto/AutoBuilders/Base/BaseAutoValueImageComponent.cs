using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Base;

public class BaseAutoValueImageComponent : BaseAutoValueComponent<string>
{
    [Parameter]
    public int MinImageWidth { get; set; }

    [Parameter]
    public int MinImageHeight { get; set; }

    [Parameter]
    public string? ContainerPrefix { get; set; }

    [Parameter]
    public bool ContainerPublic { get; set; } = false;

    [Parameter]
    public string ImageStyle { get; set; } = string.Empty;
}