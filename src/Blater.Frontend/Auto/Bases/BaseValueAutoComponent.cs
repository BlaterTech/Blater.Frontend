using Microsoft.AspNetCore.Components;

namespace Blater.FrontEnd.Auto.Bases;

public class BaseValueAutoComponent<TValue> : BaseAutoComponent
{
    [Parameter]
    [EditorRequired]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public string? Mask { get; set; }
    
    public virtual string ValueAsString => Value?.ToString() ?? "N/A";
}