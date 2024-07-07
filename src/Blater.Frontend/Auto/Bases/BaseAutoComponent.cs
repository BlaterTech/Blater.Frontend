using System.Diagnostics.CodeAnalysis;
using Blater.AutoModelConfigurations.Configs;
using Blater.Enumerations.AutoModel;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto.Bases;


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
    [EditorRequired]
    public AutoComponentConfiguration ComponentConfiguration { get; set; } = null!;

    [Parameter]
    public AutoFieldSize? Size { get; set; }

    [Parameter]
    public bool AllowNullableProperty { get; set; }

    [Parameter]
    public string? PlaceholderText { get; set; }
}