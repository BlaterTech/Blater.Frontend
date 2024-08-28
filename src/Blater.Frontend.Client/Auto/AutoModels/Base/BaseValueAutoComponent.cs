using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public class BaseValueAutoComponent<TValue> : BaseAutoComponent
{
    [Parameter]
    [EditorRequired]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public string? Mask { get; set; }
    
    public virtual string ValueAsString => Value?.ToString() ?? "N/A";
}