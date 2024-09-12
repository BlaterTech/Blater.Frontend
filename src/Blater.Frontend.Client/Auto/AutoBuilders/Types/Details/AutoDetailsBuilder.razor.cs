using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;

public partial class AutoDetailsBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    [Parameter]
    public bool Loading { get; set; }


    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Details;
    public override bool HasLabel { get; set; }

    private AutoDetailsConfiguration DetailsConfiguration { get; set; } = default!;

    protected override void LoadModelConfig()
    {
        var autoDetails = FindModelConfig<IAutoDetailsConfiguration>();
        DetailsConfiguration = autoDetails.DetailsConfiguration;
    }

    private RenderFragment RenderComponents(AutoDetailsAutoComponentConfiguration componentConfiguration, bool isTable = true) => builder =>
    {
        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);

        if (isTable)
        {
            easyRenderTreeBuilder
                       .OpenElement("tr")
                       .AddContent(trBuilder =>
                        {
                            if (componentConfiguration.AutoComponentType != AutoDetailsComponentType.AutoTitle)
                            {
                                trBuilder
                                   .OpenElement("td")
                                   .AddContent(tdBuilder =>
                                    {
                                        tdBuilder
                                           .AddContent(
                                                ComponentLocalizationService.GetLabelNameValue(componentConfiguration.LabelName, componentConfiguration.Property));
                                    })
                                   .Close();
                                if (!string.IsNullOrEmpty(componentConfiguration.ExtraStyle))
                                {
                                    trBuilder
                                       .OpenElement("td")
                                       .AddAttribute("style", componentConfiguration.ExtraStyle)
                                       .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, componentConfiguration); })
                                       .Close();
                                }
                                else
                                {
                                    trBuilder
                                       .OpenElement("td")
                                       .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, componentConfiguration); })
                                       .Close();
                                }
                            }
                            else
                            {
                                trBuilder
                                   .OpenElement("td")
                                   .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, componentConfiguration); })
                                   .Close();
                                trBuilder
                                   .OpenElement("td")
                                   .Close();
                            }
                        })
                       .Close();
        }
        else
        {
            CreateGenericComponent(easyRenderTreeBuilder, componentConfiguration);
        }
    };
}