using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public partial class AutoFormTimelineBuilder<T> : AutoFormBuilder<T> where T : BaseDataModel
{
    [Parameter]
    public EventCallback<int> OnCurrentStepChanged { get; set; }
    
    public override AutoComponentDisplayType DisplayType { get; set; }
    public override bool HasLabel { get; set; } = true;

    private int _minValue;
    private int _maxValue;
    private int _currentStep = 0;

    private Dictionary<int, string> Steps
        => FormTimelineConfiguration
          .Steps
          .GetHasFlagValue(DisplayType | AutoComponentDisplayType.FormTimeline)
           ?? [];

    private AutoFormTimelineConfiguration FormTimelineConfiguration { get; set; } = default!;

    protected override void LoadModelConfig()
    {
        var formTimelineConfiguration = FindModelConfig<IAutoFormTimelineConfiguration>();
        FormTimelineConfiguration = formTimelineConfiguration.FormTimelineConfiguration;

        _minValue = Steps.FirstOrDefault().Key;
        _maxValue = Steps.LastOrDefault().Key;
    }

    protected override void OnInitialized()
    {
        DisplayType = EditMode ? AutoComponentDisplayType.FormTimelineEdit : AutoComponentDisplayType.FormTimelineCreate;
    }

    public int GetCurrentStep()
        => _currentStep;

    public async Task SetCurrentStep(int value)
    {
        _currentStep = value;
        await OnCurrentStepChanged.InvokeAsync(_currentStep);
    }
    
    private RenderFragment RenderAutoFormBuilder() => builder =>
    {
        var stepConfigurations = FormTimelineConfiguration
                                .Steps
                                .GetHasFlagValue(DisplayType | AutoComponentDisplayType.FormTimeline);

        var stepConfiguration = stepConfigurations?.GetValueOrDefault(_currentStep);

        if (stepConfiguration == null)
        {
            Logger.LogError("Not found CurrentStep {CurrentStep} in AutoFormTimelineStepConfiguration", _currentStep);
            return;
        }

        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);
        
        easyRenderTreeBuilder
           .OpenComponent<AutoFormBuilder<T>>()
           .AddAttribute(nameof(Id), Id)
           .AddAttribute(nameof(Model), Model)
           .AddAttribute(nameof(EnableActionsButtons), false)
           .AddAttribute(nameof(EnablePrincipalTitle), false)
           .AddAttribute(nameof(RenderOnlyGroup), (true, _currentStep))
           .Close();
    };

    private async Task GoBackCallback()
    {
        if (_currentStep == _minValue)
        {
            Logger.LogInformation("GoBack");
            //await NavigationService.GoBack();
        }

        if (_currentStep <= _maxValue && _currentStep > _minValue)
        {
            _currentStep--;
            Logger.LogInformation("BackStep {Step}", _currentStep);
            await OnCurrentStepChanged.InvokeAsync(_currentStep);
        }
        
        //todo: pensar melhor em como fazer o processo de steps
        StateHasChanged();
    }

    private async Task NextCallback()
    {
        await Task.Delay(1);
        //var isValid = await Validate();

        /*if (isValid)
        {
            if (CurrentStep == _maxValue)
            {
                //todo: save model in db
            }
            else
            {
                CurrentStep++;
            }
        }*/
        
        if (_currentStep == _maxValue)
        {
            //todo: save model in db
            Logger.LogInformation("Save model in database");
        }
        else
        {
            _currentStep++;
            Logger.LogInformation("NextStep {Step}", _currentStep);
            await OnCurrentStepChanged.InvokeAsync(_currentStep);
        }
        
        StateHasChanged();
    }
}