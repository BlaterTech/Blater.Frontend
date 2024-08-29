using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public class BaseComponentInput : ComponentBase
{
    [Parameter]
    public bool HasValidationError { get; set; }

    [Parameter]
    public string? ValidationErrorMessage { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public bool Disabled { get; set; }
    
    [Parameter]
    public bool IsReadOnly { get; set; }

    protected Guid InputId { get; private set; } = Guid.NewGuid();
}