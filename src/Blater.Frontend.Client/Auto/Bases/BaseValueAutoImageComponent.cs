using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Bases;

public class BaseValueAutoImageComponent : BaseValueAutoComponent<string>
{
    [Parameter]
    public int MinImageWidth { get; set; }

    [Parameter]
    public int MinImageHeight { get; set; }
    
    [Parameter]
    public string? ContainerPrefix { get; set; }

    [Parameter]
    public bool ContainerPublic { get; set; } = false;
}