using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Frontend.Client.Extensions;
using Blater.Frontend.Client.Interfaces;
using Blater.Interfaces;
using Blater.Models;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Base;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
public abstract class BaseAutoComponentBuilder<T> : ComponentBase where T : BaseDataModel
{
    #region Injections

    [Inject]
    public ILogger<BaseAutoComponentBuilder<T>> Logger { get; set; } = null!;

    [Inject]
    public ILocalizationService LocalizationService { get; set; } = null!;

    [Inject]
    public INavigationService NavigationService { get; set; } = null!;

    [Inject]
    public IBlaterDatabaseRepository<T> DataRepository { get; set; } = null!;

    [Inject]
    public AutoConfigurations Configurations { get; set; } = null!;
    
    #endregion

    #region Parameters

    [Parameter]
    public T? Model { get; set; }

    [Parameter]
    public Guid? Id { get; set; }

    #endregion

    #region AbstractProps

    public abstract AutoComponentDisplayType DisplayType { get; set; }

    public abstract bool HasLabel { get; set; }

    #endregion
    
    public bool EditMode { get; private set; }

    protected abstract void LoadModelConfig();

    protected override async Task OnInitializedAsync()
    {
        if ((Id != null && Id != Guid.Empty) || (Model != null && Model?.Id != Guid.Empty))
        {
            EditMode = true;
        }

        Model ??= Activator.CreateInstance<T>();

        LoadModelConfig();

        ILocalizationService.LocalizationChanged += () => { InvokeAsync(StateHasChanged); };

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
            /*var databaseModel = await DataRepository.FindOne(BlaterId.Empty);

            if (databaseModel == null)
            {
                Logger.LogError("Failed to find item");
                return;
            }

            Model = databaseModel;*/
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        //todo: refactor this
        var value = Model?.Id ?? BlaterId.New(typeof(T).FullName!.SanitizeString());
        var seq = 0;
        builder.OpenComponent<CascadingValue<Guid>>(seq++);
        builder.AddAttribute(seq++, "Value", value.GuidValue);
        builder.AddAttribute(seq++, "Name", "ParentId");
        builder.AddAttribute(seq++, "IsFixed", true);
        builder.AddAttribute(seq, "ChildContent", (RenderFragment)(cascadingValueBuilder =>
        {
            var easyRenderTreeBuilder = new EasyRenderTreeBuilder(cascadingValueBuilder);
            BuildComponent(easyRenderTreeBuilder);
        }));

        builder.CloseComponent();
    }

    protected abstract void BuildComponent(EasyRenderTreeBuilder builder);

    protected void CreateGenericComponent(EasyRenderTreeBuilder builder, BaseAutoComponentConfiguration componentConfiguration)
    {
        var componentBuilder = AutoComponentsBuilders.GetComponentBuilder(componentConfiguration);
        if (componentBuilder is null)
        {
            Logger.LogError("Failed to get component builder for {ComponentConfiguration}", componentConfiguration);
            return;
        }

        var propertyInfo = componentConfiguration.Property;
        var componentBuilderType = componentBuilder.GetType();

        var componentRenderBuilder = builder.OpenComponent(componentBuilderType);

        componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.TypeName}", propertyInfo.PropertyType.Name);
        componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.Size}", componentConfiguration.Size);
        componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.ComponentConfiguration}", componentConfiguration);
        componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.ExtraClass}", componentConfiguration.ExtraClass);
        componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.ExtraStyle}", componentConfiguration.ExtraStyle);

        var propertyValue = propertyInfo.GetValue(Model) ?? propertyInfo.PropertyType.GetDefaultValue();
        if (propertyValue != null)
        {
            componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.Value}", propertyValue);
        }

        if (HasLabel)
        {
            componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.LabelName}", componentConfiguration.LabelName);
        }

        var compType = componentConfiguration.AutoComponentType;

        if (compType == null)
        {
            Logger.LogError("Failed to get component type for {ComponentConfiguration}", componentConfiguration);
            return;
        }

        if (compType.IsFormInput() && compType.HasValueChanged)
        {
            componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.EditMode}", EditMode);
            componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.PlaceholderText}", componentConfiguration.Placeholder);
            componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.Disabled}", componentConfiguration.Disable);
            componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.IsReadOnly}", componentConfiguration.IsReadOnly);
            componentRenderBuilder.AddAttribute($"{ParameterAutoComponent.ValueChanged}", componentConfiguration.OnValueChanged);
        }

        componentRenderBuilder.Close();
    }
}