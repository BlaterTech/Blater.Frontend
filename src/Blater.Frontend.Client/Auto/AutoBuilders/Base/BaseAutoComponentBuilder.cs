using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
    public bool EditMode { get; private set; }

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

    protected void CreateGenericComponent(EasyRenderTreeBuilder builder, BaseComponentConfiguration configuration, AutoComponentDisplayType displayType)
    {
        var componentBuilder = AutoComponentsBuilders.GetComponentBuilder(configuration, displayType);
        if (componentBuilder is null)
        {
            Logger.LogError("Failed to get component builder for {ComponentConfiguration}", configuration);
            return;
        }

        var propertyInfo = configuration.Property;
        var componentBuilderType = componentBuilder.GetType();

        var componentRenderBuilder = builder.OpenComponent(componentBuilderType);

        componentRenderBuilder.AddAttribute("TypeName", propertyInfo.PropertyType.Name);
        componentRenderBuilder.AddAttribute("Size", configuration.Sizes.GetValueOrDefault(DisplayType));
        componentRenderBuilder.AddAttribute("ComponentConfiguration", configuration);
        componentRenderBuilder.AddAttribute("ExtraClass", configuration.ExtraClass);
        componentRenderBuilder.AddAttribute("ExtraStyle", configuration.ExtraStyle);

        var propertyValue = propertyInfo.GetValue(Model) ?? propertyInfo.PropertyType.GetDefaultValue();
        if (propertyValue != null)
        {
            componentRenderBuilder.AddAttribute("Value", propertyValue);
        }

        if (HasLabel)
        {
            componentRenderBuilder.AddAttribute("LabelName", configuration.LabelName);
        }

        var compType = configuration.AutoComponentTypes
                                    .FirstOrDefault(x => x.Key.HasFlag(displayType))
                                    .Value;

        if (compType == null)
        {
            Logger.LogError("Failed to get component type for {ComponentConfiguration}", configuration);
            return;
        }

        if (compType.IsFormInput() && compType.HasValueChanged)
        {
            componentRenderBuilder.AddAttribute("EditMode", EditMode);
            componentRenderBuilder.AddAttribute("PlaceholderText", configuration.Placeholder);
            componentRenderBuilder.AddAttribute("Disabled", configuration.Disable);
            componentRenderBuilder.AddAttribute("IsReadOnly", configuration.IsReadOnly);

            if (configuration.ValueChanged == null)
            {
                configuration.ValueChanged = CreateGenericValueChanged(propertyInfo);
            }

            componentRenderBuilder.AddAttribute("ValueChanged", configuration.ValueChanged);
        }

        componentRenderBuilder.Close();
    }

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
}