using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.EasyRenderTree;
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
    
    protected AutoModelConfiguration ModelConfiguration { get; set; } = null!;
    protected bool EditMode { get; private set; }
    
    private void LoadModelConfig()
    {
        var modelConfiguration = AutoConfigurations.Configurations.GetValueOrDefault(typeof(T));

        if (modelConfiguration is null)
        {
            Logger.LogWarning("No configuration found for model {ModelName} in the FieldConfigurations", Model!.GetType().Name);
            return;
        }

        ModelConfiguration = modelConfiguration;
    }
    
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
        //todo: ajustar para buscar corretamente
        if (EditMode)
        {
            var databaseModel = await DataRepository.FindOne(BlaterId.Empty);

            if (databaseModel == null)
            {
                Logger.LogError("Failed to find item");
                return;
            }

            Model = databaseModel;
        }
    }
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        try
        {
            builder.OpenComponent<CascadingValue<Guid>>(1);
            builder.AddAttribute(2, "Value", Model?.Id!);
            builder.AddAttribute(3, "Name", "ParentId");
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "ChildContent", (RenderFragment)(cascadingValueBuilder =>
            {
                var easyRenderTreeBuilder = new EasyRenderTreeBuilder(cascadingValueBuilder);
                BuildComponent(easyRenderTreeBuilder);
            }));

            builder.CloseComponent();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to build the fields");
        }
    }
    
    protected abstract void BuildComponent(EasyRenderTreeBuilder builder);

    protected void CreateGenericComponent(EasyRenderTreeBuilder builder, BasePropertyConfiguration configuration)
    {
        var componentBuilder = AutoComponentsBuilders.GetComponentBuilder(configuration, DisplayType);
        if (componentBuilder is null)
        {
            Logger.LogError("Failed to get component builder for {ComponentConfiguration}", configuration);
            return;
        }
        
        var propertyInfo = configuration.Property;
        var componentBuilderType = componentBuilder.GetType();
        
        var componentRenderBuilder = builder.OpenComponent(componentBuilderType);
        
        componentRenderBuilder.AddAttribute("ComponentConfiguration", configuration);
        componentRenderBuilder.AddAttribute("TypeName", propertyInfo.PropertyType.Name);
        componentRenderBuilder.AddAttribute("Size", configuration.Sizes[DisplayType]);
        componentRenderBuilder.AddAttribute("ExtraClass", configuration.ExtraClass);
        componentRenderBuilder.AddAttribute("ExtraStyle", configuration.ExtraStyle);
        
        if (HasLabel)
        {
            componentRenderBuilder.AddAttribute("LabelName", configuration.LabelName);
        }
        
        var compType = configuration.AutoComponentTypes[DisplayType];
        if (compType.IsFormInput() && compType.HasValueChanged)
        {
            componentRenderBuilder.AddAttribute("EditMode", EditMode);
            componentRenderBuilder.AddAttribute("ValueChanged", configuration.OnValueChanged);
            componentRenderBuilder.AddAttribute("PlaceholderText", configuration.Placeholder);
            componentRenderBuilder.AddAttribute("Disabled", configuration.Disable);
            componentRenderBuilder.AddAttribute("IsReadOnly", configuration.IsReadOnly);
        }
        
        componentRenderBuilder.Close();
    }
}