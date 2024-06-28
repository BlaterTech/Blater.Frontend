using System.Diagnostics.CodeAnalysis;
using Blater.AutoModelConfigurations.Configs;
using Blater.Enumerations.AutoModel;
using Blater.FrontEnd.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.FrontEnd.Auto.AutoBuilders;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class AutoDynamicComponentBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    [Parameter]
    public override AutoComponentDisplayType DisplayType { get; set; }

    [Parameter]
    [EditorRequired]
    public AutoComponentConfiguration ComponentConfiguration { get; set; } = null!;

    [Parameter]
    public override bool HasLabel { get; set; }

    protected override void BuildComponent(EasyRenderTreeBuilder builder)
    {
        CreateGenericComponent(builder, ComponentConfiguration);
    }
}