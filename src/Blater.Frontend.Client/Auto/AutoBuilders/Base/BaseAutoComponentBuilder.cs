using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Contracts.Bases;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Frontend.Client.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Base;

public abstract class BaseAutoComponentBuilder<T> : ComponentBase where T : BaseFrontendModel
{
    #region Injections

    [Inject]
    public ILogger<BaseAutoComponentBuilder<T>> Logger { get; set; } = null!;

    [Inject]
    public ILocalizationService LocalizationService { get; set; } = null!;

    [Inject]
    public INavigationService NavigationService { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public AutoConfigurations AutoConfigurations { get; set; } = null!;

    #endregion

    #region Parameters

    [Parameter]
    public T? Model { get; set; }

    [Parameter]
    public Ulid? Id { get; set; }

    #endregion

    #region Events

    [Parameter]
    public EventCallback<T> EditCallback { get; set; }

    #endregion

    #region AbstractProps

    public abstract AutoComponentDisplayType DisplayType { get; set; }

    public abstract bool HasLabel { get; set; }

    #endregion

    public Ulid CascadingValue => Model?.Id ?? Ulid.NewUlid();
    public bool EditMode { get; private set; }

    protected abstract void LoadModelConfig();

    protected TConfig FindModelConfig<TConfig>()
    {
        var modelType = typeof(T);
        if (AutoConfigurations.Configurations.TryGetValue(modelType, out var value))
        {
            if (value is TConfig configValue)
            {
                return configValue;
            }
        }

        Logger.LogError("Model {Name} does not implement IAutoFormConfiguration<{TypeName}>", modelType.Name, modelType.Name);
        throw new InvalidCastException($"Model does not implement IAutoFormConfiguration<{modelType.Name}>");
    }

    protected override async Task OnInitializedAsync()
    {
        if ((Id != null && Id != Ulid.Empty) || (Model != null && Model?.Id != Ulid.Empty))
        {
            EditMode = true;
        }

        Model ??= Activator.CreateInstance<T>();

        LoadModelConfig();

        LocalizationService.LocalizationChanged += () => { InvokeAsync(StateHasChanged); };

        AutoConfigurations.ModelsChanged += () =>
        {
            LoadModelConfig();
            InvokeAsync(StateHasChanged);
        };

        //Try to load from database
        await Task.Delay(1);
        //todo: refactor this
        if (EditMode)
        {
            /*var databaseModel = await DataRepository.FindOne(Ulid.Empty);

            if (databaseModel == null)
            {
                Logger.LogError("Failed to find item");
                return;
            }

            Model = databaseModel;*/
        }
    }

    protected void CreateGenericComponent(
        EasyRenderTreeBuilder builder,
        object propertyConfigurationInstance)
    {
        if (propertyConfigurationInstance is not IBaseAutoPropertyConfiguration propertyConfiguration)
        {
            throw new Exception("");
        }

        var componentBuilder = AutoComponentsBuilders.GetComponentBuilder(propertyConfiguration);
        if (componentBuilder is null)
        {
            Logger.LogError("Failed to get component builder for {ComponentConfiguration}", propertyConfiguration);
            return;
        }

        var propertyInfo = propertyConfiguration.Property;
        var componentBuilderType = componentBuilder.GetType();

        var componentRenderBuilder = builder.OpenComponent(componentBuilderType);

        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.TypeName), propertyInfo.Name);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.Size), propertyConfiguration.Size);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.AutoPropertyConfiguration), propertyConfiguration);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.ExtraClass), propertyConfiguration.ExtraClass);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.ExtraStyle), propertyConfiguration.ExtraStyle);

        if (!propertyConfiguration.EnableDefaultValue)
        {
            var propertyValue = propertyInfo.GetValue(Model) ?? propertyInfo.PropertyType.GetDefaultValue();
            if (propertyValue != null)
            {
                componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.Value), propertyValue);
            }
        }

        if (HasLabel)
        {
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.LabelName), GetLabelNameValue(propertyConfiguration));
        }

        var compType = propertyConfiguration.AutoComponentType;

        if (compType == null)
        {
            Logger.LogError("Failed to get component type for {ComponentConfiguration}", propertyConfiguration);
            return;
        }

        if (compType.IsFormInput() && compType.HasValueChanged)
        {
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.EditMode), EditMode);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.PlaceholderText), GetPlaceholderValue(propertyConfiguration));
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.Disabled), propertyConfiguration.Disable);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.IsReadOnly), propertyConfiguration.IsReadOnly);

            var propConfigurationType = propertyConfigurationInstance.GetType();
            var valueChangedProp = propConfigurationType.GetProperty(nameof(IBaseAutoPropertyConfigurationValue<string>.OnValueChanged));
            var valueChangedValue = valueChangedProp?.GetValue(propertyConfigurationInstance);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.OnValueChanged), valueChangedValue);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.ModelInstance), Model);
        }

        componentRenderBuilder.Close();
    }

    protected string GetTitleValue<TConfiguration>(TConfiguration configuration) where TConfiguration : BaseAutoConfiguration
    {
        var value = LocalizationService.GetValueOrDefault(configuration.LocalizationId!);
        if (string.IsNullOrWhiteSpace(value))
        {
            value = configuration.Title;
        }

        return value;
    }

    protected string GetGroupTitleValue<TConfiguration>(TConfiguration configuration) where TConfiguration : BaseAutoGroupConfiguration
    {
        var value = LocalizationService.GetValueOrDefault(configuration.LocalizationId!);
        if (string.IsNullOrWhiteSpace(value))
        {
            value = configuration.Title;
        }

        return value;
    }

    protected string GetLabelNameValue(IBaseAutoPropertyConfiguration propertyConfiguration)
    {
        var value = LocalizationService.GetValueOrDefault(propertyConfiguration.LabelNameLocalizationId!);
        if (string.IsNullOrWhiteSpace(value))
        {
            value = propertyConfiguration.LabelName;
        }

        return value!;
    }

    protected string GetPlaceholderValue(IBaseAutoPropertyConfiguration propertyConfiguration)
    {
        var value = LocalizationService.GetValueOrDefault(propertyConfiguration.PlaceHolderLocalizationId!);
        if (string.IsNullOrWhiteSpace(value))
        {
            value = propertyConfiguration.Placeholder;
        }

        return value!;
    }
}