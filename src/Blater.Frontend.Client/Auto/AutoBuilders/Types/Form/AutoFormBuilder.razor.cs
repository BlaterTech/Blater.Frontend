using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Frontend.Client.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public partial class AutoFormBuilder<T, TValidator> : BaseAutoComponentBuilder<T>
    where T : BaseFrontendModel
    where TValidator : BaseModelValidator<T>
{
    [Parameter]
    public EventCallback<T> AfterUpsert { get; set; }

    [Parameter]
    public Func<T, Task<bool>>? BeforeUpsert { get; set; }

    [Parameter]
    public bool Processing { get; set; }

    [Parameter]
    public bool EnablePrincipalTitle { get; set; } = true;

    [Parameter]
    public bool EnableActionsButtons { get; set; } = true;

    [Parameter]
    public (bool enabled, int index) RenderOnlyGroup { get; set; }

    [Parameter]
    public AutoFormConfiguration<T>? FormConfiguration { get; set; }

    [Parameter]
    public TValidator? ModelValidator { get; set; }
    
    public override AutoComponentDisplayType DisplayType { get; set; }
    public override bool HasLabel { get; set; } = true;

    MudForm _form = null!;
    private AutoAvatarModelConfiguration? AvatarModelConfiguration { get; set; }
    private AutoGridConfiguration? GridConfiguration { get; set; }
    private AutoFormActionConfiguration ActionConfiguration { get; set; } = new();

    private bool IsValid { get; set; }

    private static async Task Upsert()
    {
        await Task.Delay(1);
    }

    protected async Task<bool> Validate()
    {
        await _form.Validate();

        return IsValid;
    }

    protected override void OnInitialized()
    {
        DisplayType = EditMode ? AutoComponentDisplayType.FormEdit : AutoComponentDisplayType.FormCreate;
    }

    protected override void LoadModelConfig()
    {
        //var autoValidator = FindModelConfig<IAutoValidatorConfiguration<T>>();

        if (FormConfiguration == null)
        {
            var autoForm = FindModelConfig<IAutoFormConfiguration<T>>();
            FormConfiguration = autoForm.FormConfiguration;
        }

        AvatarModelConfiguration = FormConfiguration
                                  .AvatarConfiguration
                                  .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form);

        ActionConfiguration = FormConfiguration
                             .ActionConfiguration
                             .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form)
                              ?? new AutoFormActionConfiguration();

        GridConfiguration = FormConfiguration
                           .GridConfigurations
                           .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form)
                            ?? new AutoGridConfiguration();

        if (ModelValidator != null)
        {
            return;
        }
        
        var modelType = typeof(T);
        if (AutoConfigurations.Validators.TryGetValue(modelType, out var value))
        {
            if (value is BaseModelValidator<T> modelValidator)
            {
                ModelValidator = (TValidator?)modelValidator;
            }
        }
    }

    private RenderFragment RenderFormInputs(int breakpointGroupValue) => builder =>
    {
        var groupConfigurations = FormConfiguration!
                                 .Groups
                                 .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form);

        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);

        if (groupConfigurations == null)
        {
            Logger.LogError("No group configurations founded");
            return;
        }

        if (RenderOnlyGroup.enabled)
        {
            var index = RenderOnlyGroup.index - 1;
            var groupConfiguration = groupConfigurations[index];
            RenderGroupWithSubGroups(groupConfiguration);
            return;
        }

        foreach (var groupConfiguration in groupConfigurations)
        {
            RenderGroupWithSubGroups(groupConfiguration);
        }

        return;

        void RenderGroupWithSubGroups(AutoFormGroupConfiguration<T> groupConfiguration)
        {
            Group(groupConfiguration);

            var subGroups = groupConfiguration
                           .SubGroups
                           .GetHasFlagValue(DisplayType | AutoComponentDisplayType.Form);

            if (subGroups?.Count > 0)
            {
                SubGroup(subGroups);
            }
        }

        void Group(AutoFormGroupConfiguration<T> groupConfiguration)
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
                       .AddChildContent(builderTextContent =>
                                            builderTextContent
                                               .AddContent(GetGroupTitleValue(groupConfiguration)))
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

                itemBuilder
                   .AddChildContent(mudItemContentBuilder => { CreateGenericComponent(mudItemContentBuilder, componentConfiguration); });

                itemBuilder.Close();
            }
        }

        void SubGroup(List<AutoFormGroupConfiguration<T>> subGroupConfigurations)
        {
            foreach (var subGroupConfiguration in subGroupConfigurations)
            {
                Group(subGroupConfiguration);
            }
        }
    };
}