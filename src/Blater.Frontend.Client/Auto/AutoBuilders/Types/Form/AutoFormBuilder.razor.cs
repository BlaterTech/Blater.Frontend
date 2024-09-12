using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Validator;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public partial class AutoFormBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    [Parameter]
    public EventCallback<T> AfterUpsert { get; set; }

    [Parameter]
    public Func<T, Task<bool>>? BeforeUpsert { get; set; }

    [Parameter]
    public bool Processing { get; set; }

    public override AutoComponentDisplayType DisplayType { get; set; }
    public override bool HasLabel { get; set; } = true;

    private AutoFormConfiguration Configuration { get; set; } = default!;
    private AutoAvatarModelConfiguration? AvatarModelConfiguration { get; set; }
    private ModelValidator<T>? ModelValidator { get; set; }
    private AutoGridConfiguration? GridConfiguration { get; set; }
    private AutoFormActionConfiguration ActionConfiguration { get; set; } = new();

    private static async Task Upsert()
    {
        await Task.Delay(1);
    }

    protected override void OnInitialized()
    {
        DisplayType = EditMode ? AutoComponentDisplayType.FormEdit : AutoComponentDisplayType.FormCreate;
    }

    protected override void LoadModelConfig()
    {
        var autoForm = FindModelConfig<IAutoFormConfiguration>();
        var autoValidator = FindModelConfig<IAutoValidatorConfiguration<T>>();
        Configuration = autoForm.FormConfiguration;

        AvatarModelConfiguration = Configuration
                                  .AvatarConfiguration
                                  .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form);

        ActionConfiguration = Configuration
                             .ActionConfiguration
                             .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form)
                              ?? new AutoFormActionConfiguration();

        GridConfiguration = Configuration
                           .GridConfigurations
                           .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form)
                            ?? new AutoGridConfiguration();

        ModelValidator = autoValidator
                        .ValidatorConfiguration
                        .Validators
                        .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form);
    }

    private RenderFragment RenderFormInputs(int breakpointGroupValue) => builder =>
    {
        var groupConfigurations = Configuration
                                 .GroupConfigurations
                                 .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form);
        
        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);

        foreach (var groupConfiguration in groupConfigurations ?? [])
        {
            easyRenderTreeBuilder
               .OpenComponent<MudItem>()
               .AddAttribute(nameof(MudItem.xs), breakpointGroupValue)
               .AddChildContent(titleGroupBuilder =>
                {
                    titleGroupBuilder
                       .OpenComponent<MudText>()
                       .AddAttribute(nameof(MudText.Typo), Typo.h4)
                       .AddAttribute(nameof(MudText.Color), Color.Inherit)
                       .AddChildContent(builderTextContent => builderTextContent.AddContent(ComponentLocalizationService.GetTitleValue(groupConfiguration.Title)))
                       .Close();
                })
               .Close();

            var componentConfigurations = groupConfiguration.ComponentConfigurations;

            foreach (var componentConfiguration in componentConfigurations)
            {
                var itemBuilder = easyRenderTreeBuilder.OpenComponent<MudItem>();

                if (componentConfiguration.Breakpoints.Count > 0)
                {
                    foreach (var (breakpoint, value) in componentConfiguration.Breakpoints)
                    {
                        itemBuilder.AddAttribute(breakpoint.ToString(), value);
                    }
                }
                else
                {
                    itemBuilder.AddAttribute(nameof(MudItem.md), 6);
                    itemBuilder.AddAttribute(nameof(MudItem.xs), 12);
                }

                itemBuilder.AddChildContent(mudItemContentBuilder => { CreateGenericComponent(mudItemContentBuilder, componentConfiguration); });

                itemBuilder.Close();
            }
        }
    };
}