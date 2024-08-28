using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Bases;

public class BaseAutoComponent : BaseComponentInput
{
    [Parameter]
    [EditorRequired]
    public string ExtraClass { get; set; } = null!;

    [Parameter]
    public string TypeName { get; set; } = null!;

    [Parameter]
    public string? LabelName { get; set; }

    [Parameter]
    public bool EditMode { get; set; }
    
    [Parameter]
    public bool AllowNullableProperty { get; set; }

    [Parameter]
    public string? PlaceholderText { get; set; }
}