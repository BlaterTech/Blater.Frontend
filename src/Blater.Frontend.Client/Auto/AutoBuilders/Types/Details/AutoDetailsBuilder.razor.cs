using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;
using Blater.Frontend.Client.Contracts.Bases;
using Blater.Frontend.Client.EasyRenderTree;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;

public partial class AutoDetailsBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseFrontendModel
{
    [Parameter]
    public bool Loading { get; set; }


    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Details;
    public override bool HasLabel { get; set; }

    private AutoDetailsConfiguration<T> DetailsConfiguration { get; set; } = default!;

    protected override void LoadModelConfig()
    {
        var autoDetails = FindModelConfig<IAutoDetailsConfiguration<T>>();
        DetailsConfiguration = autoDetails.DetailsConfiguration;
    }

    protected RenderFragment RenderComponents(IBaseAutoPropertyConfiguration propertyConfiguration, bool isTable = true) => builder =>
    {
        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);

        if (isTable)
        {
            easyRenderTreeBuilder
               .OpenElement("tr")
               .AddContent(trBuilder =>
                {
                    if (propertyConfiguration.AutoComponentType != AutoComponentType.AutoTitle)
                    {
                        trBuilder
                           .OpenElement("td")
                           .AddContent(tdBuilder =>
                            {
                                tdBuilder
                                   .AddContent(
                                        GetLabelNameValue(propertyConfiguration));
                            })
                           .Close();
                        if (!string.IsNullOrEmpty(propertyConfiguration.ExtraStyle))
                        {
                            trBuilder
                               .OpenElement("td")
                               .AddAttribute("style", propertyConfiguration.ExtraStyle)
                               .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, propertyConfiguration); })
                               .Close();
                        }
                        else
                        {
                            trBuilder
                               .OpenElement("td")
                               .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, propertyConfiguration); })
                               .Close();
                        }
                    }
                    else
                    {
                        trBuilder
                           .OpenElement("td")
                           .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, propertyConfiguration); })
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
            CreateGenericComponent(easyRenderTreeBuilder, propertyConfiguration);
        }
    };

    private string GetAvatarTitleValue(AutoAvatarModelConfiguration configuration)
    {
        var value = LocalizationService.GetValueOrDefault(configuration.LocalizationId!);
        if (string.IsNullOrWhiteSpace(value))
        {
            value = configuration.Title;
        }

        return value;
    }

    private string GetAvatarSubTitleValue(AutoAvatarModelConfiguration configuration)
    {
        var value = LocalizationService.GetValueOrDefault(configuration.SubTitleLocalizationId);
        if (string.IsNullOrWhiteSpace(value))
        {
            value = configuration.SubTitle;
        }

        return value;
    }
}