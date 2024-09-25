using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;
using Blater.Frontend.Client.Contracts.Bases;
using Blater.Frontend.Client.EasyRenderTree;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public partial class AutoFormTimelineBuilder<T, TValidator> : BaseAutoComponentBuilder<T>
    where T : BaseFrontendModel
    where TValidator : BaseModelValidator<T>
{
    [Parameter]
    public EventCallback<int> OnCurrentStepChanged { get; set; }
    
    [Parameter]
    public TValidator? ModelValidator { get; set; }

    public override AutoComponentDisplayType DisplayType { get; set; }
    public override bool HasLabel { get; set; } = true;

    private int _minValue;
    private int _maxValue;
    private int _currentStep = 1;

    private List<AutoFormTimelineStepConfiguration<T>> Steps
        => FormTimelineConfiguration
          .Steps
          .GetHasFlagValue(DisplayType | AutoComponentDisplayType.FormTimeline)
           ?? [];

    private AutoFormTimelineConfiguration<T> FormTimelineConfiguration { get; set; } = new("Default");

    protected override void LoadModelConfig()
    {
        var formTimelineConfiguration = FindModelConfig<IAutoFormTimelineConfiguration<T>>();
        FormTimelineConfiguration = formTimelineConfiguration.FormTimelineConfiguration;
        
        _minValue = Steps.FirstOrDefault()?.Key ?? 1;
        _maxValue = Steps.LastOrDefault()?.Key ?? 1;
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

        var stepConfiguration = stepConfigurations?.FirstOrDefault(x => x.Key == _currentStep);

        if (stepConfiguration == null)
        {
            Logger.LogError("Not found CurrentStep {CurrentStep} in AutoFormTimelineStepConfiguration", _currentStep);
            return;
        }

        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);

        easyRenderTreeBuilder
           .OpenComponent<AutoFormBuilder<T, TValidator>>()
           .AddAttribute(nameof(Id), Id)
           .AddAttribute(nameof(Model), Model)
           .AddAttribute(nameof(ModelValidator), ModelValidator)
           .AddAttribute(nameof(AutoFormBuilder<T, TValidator>.EnableActionsButtons), false)
           .AddAttribute(nameof(AutoFormBuilder<T, TValidator>.EnablePrincipalTitle), false)
           .AddAttribute(nameof(AutoFormBuilder<T, TValidator>.FormConfiguration), stepConfiguration.AutoFormConfiguration)
           .AddAttribute(nameof(AutoFormBuilder<T, TValidator>.RenderOnlyGroup), (true, _currentStep))
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

    private string GetStepValue(string value)
    {
        var localizationValue = LocalizationService.GetValueOrDefault($"Blater-Auto{DisplayType}-{value}");
        if (string.IsNullOrWhiteSpace(localizationValue))
        {
            localizationValue = value;
        }

        return localizationValue;
    }
}