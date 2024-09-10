using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Details;
    public override bool HasLabel { get; set; }


    protected override void LoadModelConfig()
    {
        throw new NotImplementedException();
    }

    protected override void BuildComponent(EasyRenderTreeBuilder builder)
    {
        throw new NotImplementedException();
    }
}