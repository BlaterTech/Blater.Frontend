using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public partial class AutoFormTimelineBuilder<T> : AutoFormBuilder<T> where T : BaseDataModel
{
    [Parameter]
    public int CurrentStep { get; set; } = 1;

    public override AutoComponentDisplayType DisplayType { get; set; }
    public override bool HasLabel { get; set; } = true;


    private AutoFormTimelineConfiguration FormTimelineConfiguration { get; set; } = default!;

    protected override void LoadModelConfig()
    {
        var formTimelineConfiguration = FindModelConfig<IAutoFormTimelineConfiguration>();
        FormTimelineConfiguration = formTimelineConfiguration.FormTimelineConfiguration;
    }

    protected override void OnInitialized()
    {
        DisplayType = EditMode ? AutoComponentDisplayType.FormTimelineEdit : AutoComponentDisplayType.FormTimelineCreate;
    }

    private RenderFragment RenderAutoFormBuilder() => builder =>
    {
        var stepConfigurations = FormTimelineConfiguration
                                .Steps
                                .GetHasFlagValue(DisplayType | AutoComponentDisplayType.FormTimeline);

        var stepConfiguration = stepConfigurations?.FirstOrDefault(x => x.Step == CurrentStep);

        if (stepConfiguration == null)
        {
            Logger.LogError("Not found CurrentStep {CurrentStep} in AutoFormTimelineStepConfiguration", CurrentStep);
            return;
        }

        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);

        easyRenderTreeBuilder
           .OpenComponent<AutoFormBuilder<T>>()
           .AddAttribute(nameof(Id), Id)
           .AddAttribute(nameof(Model), Model)
           .AddAttribute(nameof(EnableActionsButtons), false)
           .AddAttribute(nameof(EnablePrincipalTitle), false)
           .Close();
    };

    private async Task GoBackCallback()
    {
        switch (CurrentStep)
        {
            case 1:
                await NavigationService.GoBack();
                break;
            case <= 7 and > 1:
                CurrentStep--;
                break;
        }

        
        //todo: pensar melhor em como fazer o processo de steps
        StateHasChanged();
    }

    private async Task NextCallback()
    {
        var isValid = await Validate();

        if (isValid)
        {
            switch (CurrentStep)
            {
                case 7:
                {
                    break;
                }
                case < 7:
                    CurrentStep++;
                    break;
            }
        }
        
        StateHasChanged();
    }
}