using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class BaseAutoComponentBuilder<T, TConfiguration> : ComponentBase where T : BaseDataModel
{
    protected virtual void CreateGenericComponent(EasyRenderTreeBuilder builder, TConfiguration componentConfiguration)
    {
    }
}