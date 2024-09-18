using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Base;

public class BaseAutoComponent : BaseAutoComponentInput
{
    [Parameter]
    public string? ExtraClass { get; set; }
    
    [Parameter]
    public string? ExtraStyle { get; set; }
    
    [Parameter]
    public string? DataFormat { get; set; }
    
    [Parameter]
    public string? LocalizationId { get; set; }

    [Parameter]
    public string TypeName { get; set; } = null!;
    
    [Parameter]
    public AutoFieldSize? Size { get; set; }

    [Parameter]
    public string? LabelName { get; set; }

    [Parameter]
    public bool EditMode { get; set; }
    
    [Parameter]
    public bool AllowNullableProperty { get; set; }

    [Parameter]
    public string? PlaceholderText { get; set; }

    [Parameter]
    [EditorRequired]
    public IBaseAutoPropertyConfiguration AutoPropertyConfiguration { get; set; } = default!;
}