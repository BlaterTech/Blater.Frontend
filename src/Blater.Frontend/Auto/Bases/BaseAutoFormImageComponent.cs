using Microsoft.AspNetCore.Components;

namespace Blater.FrontEnd.Auto.Bases;

public class BaseAutoFormImageComponent : BaseAutoFormComponent<string>
{
    [Parameter]
    public string? ContainerPrefix { get; set; }
    
    [Parameter]
    public bool ContainerPublic { get; set; } = false;
}