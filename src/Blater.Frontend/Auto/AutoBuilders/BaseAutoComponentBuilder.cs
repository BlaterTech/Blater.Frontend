using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using Blater.AutoModelConfigurations;
using Blater.AutoModelConfigurations.Configs;
using Blater.Enumerations.AutoModel;
using Blater.Extensions;
using Blater.Frontend.EasyRenderTree;
using Blater.Frontend.Models;
using Blater.Frontend.Services;
using Blater.Interfaces;
using Blater.Models;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Auto.AutoBuilders;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public abstract class BaseAutoComponentBuilder<T> : ComponentBase where T : BaseDataModel
{
    private bool _enabled = true;
    private bool _loading;

    public bool EditMode { get; private set; }

    protected AutoModelConfiguration ModelConfiguration { get; set; } = null!;

    [Inject]
    public LocalizationService Localization { get; set; } = null!;

    [Inject]
    public ILogger<BaseAutoComponentBuilder<T>> Logger { get; set; } = null!;

    [Inject]
    public NavigationService NavigationService { get; set; } = null!;

    [Inject]
    public IBlaterDatabaseStoreT<T> DataRepository { get; set; } = null!;

    public abstract AutoComponentDisplayType DisplayType { get; set; }

    [Parameter]
    public T? Model { get; set; }

    [Parameter]
    public string? ExtraStyle { get; set; }

    [Parameter]
    public string? ExtraClass { get; set; }

    [Parameter]
    public FindPropertiesAttributeOptions<T>? Options { get; set; } = new();
    
    [Parameter]
    public BlaterId? Id { get; set; }

    [Parameter]
    public bool HasEditButton { get; set; } = true;
    
    [Parameter]
    public EventCallback<T> EditCallback { get; set; }

    protected List<AutoComponentConfiguration> ComponentConfigurations { get; set; } = new();

    public bool Loading
    {
        get => _loading;
        set
        {
            _loading = value;
            StateHasChanged();
        }
    }

    public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            StateHasChanged();
        }
    }

    public abstract bool HasLabel { get; set; }

    private void LoadModelConfig()
    {
        var modelConfiguration = ModelConfigurations.AutoConfigurations.GetValueOrDefault(typeof(T));

        if (modelConfiguration is null)
        {
            Logger.LogWarning("No configuration found for model {ModelName} in the FieldConfigurations", Model!.GetType().Name);
            return;
        }

        ModelConfiguration = modelConfiguration;

        LoadComponentConfigurations();
    }

    private void LoadComponentConfigurations()
    {
        ComponentConfigurations = ModelConfiguration.ComponentConfigurations
                                                    .Where(x => x.ComponentTypes.GetValueOrDefault(DisplayType) != null)
                                                    .OrderBy(x => x.Order[DisplayType])
                                                    .ToList();
    }

    protected override async Task OnInitializedAsync()
    {
        if (Id != BlaterId.Empty)
        {
            EditMode = true;
        }

        Model ??= Activator.CreateInstance<T>();

        LoadModelConfig();
        
        LocalizationService.LocalizationChanged += () => { InvokeAsync(StateHasChanged); };

        ModelConfigurations.ModelsChanged += () =>
        {
            LoadModelConfig();
            InvokeAsync(StateHasChanged);
        };
        
        Options ??= new FindPropertiesAttributeOptions<T>();

        //Try to load from database
        if (EditMode && Id != null)
        {
            var databaseModel = await DataRepository.FindOne(Id);

            if (databaseModel.Value == null)
            {
                Logger.LogError("Failed to find item");
                return;
            }

            Model = databaseModel.Value;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        try
        {
            LoadComponentConfigurations();

            if (Model == null)
            {
                return;
            }

            builder.OpenComponent<CascadingValue<Guid>>(1);
            builder.AddAttribute(2, "Value", Model.Id);
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

    protected void CreateGenericComponent(EasyRenderTreeBuilder builder, AutoComponentConfiguration componentConfiguration)
    {
        
        var componentBuilder = AutoComponentsBuilders.GetComponentBuilder(componentConfiguration, DisplayType);

        if (componentBuilder is null)
        {
            Logger.LogError("Failed to get component builder for {ComponentConfiguration}", componentConfiguration);
            return;
        }

        var propertyInfo = componentConfiguration.Property;
        var componentBuilderType = componentBuilder.GetType();

        var componentRenderBuilder = builder.OpenComponent(componentBuilderType);

        componentRenderBuilder.AddAttribute("ComponentConfiguration", componentConfiguration);

        //LabelName
        if (HasLabel)
        {
            var labelName = Localization.Get($"{DisplayType.ToString()}-{typeof(T).Name}-{propertyInfo?.Name}");
            componentRenderBuilder.AddAttribute("LabelName", labelName);
        }

        componentRenderBuilder.AddAttribute("TypeName", propertyInfo?.PropertyType.Name);
        componentRenderBuilder.AddAttribute("Size", componentConfiguration.Sizes[DisplayType]);
        componentRenderBuilder.AddAttribute("ComponentConfiguration", componentConfiguration);

        var propertyValue = propertyInfo?.GetValue(Model) ?? propertyInfo?.PropertyType.GetDefaultValue();
        if (propertyValue != null)
        {
            componentRenderBuilder.AddAttribute("Value", propertyValue);
        }

        var compType = componentConfiguration.ComponentTypes[DisplayType];
        if (compType.IsFormInput() && compType.HasValueChanged)
        {
            componentRenderBuilder.AddAttribute("EditMode", EditMode);
            if (propertyInfo != null)
            {
                var valueChanged = CreateGenericValueChanged(propertyInfo);
                componentRenderBuilder.AddAttribute("ValueChanged", valueChanged);
            }

            componentRenderBuilder.AddAttribute("PlaceholderText", Localization.Get($"PlaceholderText-{typeof(T).Name}-{propertyInfo?.Name}"));
            componentRenderBuilder.AddAttribute("Disabled", !Enabled);
        }

        //Extra classes
        var extraClassItemBuilder = new StringBuilder();
        if (Options?.ExtraClass != null)
        {
            extraClassItemBuilder.Append(Options.ExtraClass + " ");
        }

        componentRenderBuilder.AddAttribute("ExtraClass", extraClassItemBuilder.ToString());

        foreach (var (key, value) in componentConfiguration.ExtraAttributes)
        {
            try
            {
                componentRenderBuilder.AddComponentParameter(key, value);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Failed to add extra parameter {Key} with value {Value}", key, value);
            }
        }
        AddExtraAttributes(builder, componentConfiguration);
        componentRenderBuilder.Close();
    }
    
    protected virtual void AddExtraAttributes(EasyRenderTreeBuilder builder, AutoComponentConfiguration componentConfiguration)
    {
        //TODO: usar para adicionar no form os atributos extras com os erros, usar override no autoformbuilder
    }

    protected void NavigateToEditPage()
    {
        NavigationService.Navigate($"{typeof(T).Name}/Edit/{Id}");
    }

    protected void NavigateToDetailsPage()
    {
        NavigationService.Navigate($"{typeof(T).Name}/Details/{Id}");
    }

    protected void NavigateToCreatePage()
    {
        NavigationService.Navigate($"{typeof(T).Name}/Create");
    }

    #region EventCallback Creator

    private readonly MethodInfo _makeActionMethod = typeof(BaseAutoComponentBuilder<T>).GetMethod("MakeAction",
                                                                                                  BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic)!;
    
    protected object? CreateGenericValueChanged(PropertyInfo propertyInfo)
    {
        var targetType = propertyInfo.PropertyType;

        var genericMethod = EventCallbackExtensions.EventCallbackFactoryCreate.MakeGenericMethod(targetType);

        var makeActionGenericMethod = _makeActionMethod.MakeGenericMethod(targetType);
        var action = makeActionGenericMethod.Invoke(this, [propertyInfo])!;

        var parameters = new[] { this, action };
        return genericMethod.Invoke(EventCallback.Factory, parameters);
    }

    // ReSharper disable once UnusedMember.Global
    protected Action<TEntity> MakeAction<TEntity>(PropertyInfo propertyInfo)
    {
        return Action;

        void Action(TEntity value)
        {
            try
            {
                if (propertyInfo.SetMethod is null)
                {
                    return;
                }

                propertyInfo.SetValue(Model, value);

                FieldValueChanged(propertyInfo, value);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Failed to set value to the model");
            }
        }
    }
    
    protected virtual void FieldValueChanged(PropertyInfo propertyInfo, object? newValue)
    {
        
    }
    
    #endregion
}