using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public partial class AutoDetailsTabsBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    
    [Parameter]
    public bool Loading { get; set; }
    
    public override AutoComponentDisplayType DisplayType { get; set; }
    public override bool HasLabel { get; set; }

    private AutoDetailsTabsConfiguration DetailsTabsConfiguration { get; set; } = default!;
    protected override void LoadModelConfig()
    {
        var autoDetails = FindModelConfig<IAutoDetailsTabsConfiguration>();
        DetailsTabsConfiguration = autoDetails.DetailsTabsConfiguration;
    }
}