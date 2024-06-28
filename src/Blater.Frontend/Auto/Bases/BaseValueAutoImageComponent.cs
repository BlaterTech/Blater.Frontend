using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations.AutoModel;
using Microsoft.AspNetCore.Components;

namespace Blater.FrontEnd.Auto.Bases;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
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

    protected string ImageStyle => Size switch
    {
        AutoFieldSize.Small           => "width: 1rem;      height: 1rem;",
        AutoFieldSize.Normal          => "width: 2rem;      height: 2rem;",
        AutoFieldSize.Medium          => "width: 1.5rem;    height: 1.5rem;",
        AutoFieldSize.Large           => "width: 7rem;      height: 2rem;",
        AutoFieldSize.ExtraLarge      => "width: 12rem;     height: 12rem;",
        AutoFieldSize.ExtraExtraLarge => "width: 24rem;     height: 24rem;",
        _                             => "width: 2rem;      height: 2rem;"
    };
}