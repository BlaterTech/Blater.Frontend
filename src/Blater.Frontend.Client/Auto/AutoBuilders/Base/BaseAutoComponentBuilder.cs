using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Frontend.Client.Extensions;
using Blater.Frontend.Client.Interfaces;
using Blater.Interfaces;
using Blater.JsonUtilities;
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
    public AutoConfigurations AutoConfigurations { get; set; } = null!;
    
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

    protected TConfig FindModelConfig<TConfig>()
    {
        var modelType = typeof(T);
        if(AutoConfigurations.Configurations.TryGetValue(modelType, out var value))
        {
            if (value is TConfig configValue)
            {
                return configValue;
            }
        }
        
        Logger.LogError("Model {Name} does not implement IAutoFormConfiguration<{TypeName}>", modelType.Name, modelType.Name);
        throw new InvalidCastException("Model does not implement IAutoFormConfiguration");
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

        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.TypeName), propertyInfo.Name);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.Size), componentConfiguration.Size);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.AutoComponentConfiguration), componentConfiguration);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.ExtraClass), componentConfiguration.ExtraClass);
        componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.ExtraStyle), componentConfiguration.ExtraStyle);

        var propertyValue = propertyInfo.GetValue(Model) ?? propertyInfo.PropertyType.GetDefaultValue();
        if (propertyValue != null)
        {
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.Value), propertyValue);
        }

        if (HasLabel)
        {
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.LabelName), componentConfiguration.LabelName);
        }

        var compType = componentConfiguration.AutoComponentType;

        if (compType == null)
        {
            Logger.LogError("Failed to get component type for {ComponentConfiguration}", componentConfiguration);
            return;
        }

        if (compType.IsFormInput() && compType.HasValueChanged)
        {
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.EditMode), EditMode);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.PlaceholderText), componentConfiguration.Placeholder);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.Disabled), componentConfiguration.Disable);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.IsReadOnly), componentConfiguration.IsReadOnly);
            
            var forValue = CreateGenericExpression(componentConfiguration.Property);
            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.For), forValue);

            if (componentConfiguration.OnValueChanged == null)
            {
                Console.WriteLine("CreateGenericValueChanged by Property => " + componentConfiguration.Property.Name);
                componentConfiguration.OnValueChanged = CreateGenericValueChanged(componentConfiguration.Property);
            }

            componentRenderBuilder.AddAttribute(nameof(BaseAutoFormComponent<T>.OnValueChanged), componentConfiguration.OnValueChanged);
        }

        componentRenderBuilder.Close();
    }


    protected object CreateGenericExpression(PropertyInfo propertyInfo)
    {
        var targetType = propertyInfo.DeclaringType!;
        var parameter = Expression.Parameter(targetType, "model");

        // Cria a expressão da propriedade
        var property = Expression.Property(parameter, propertyInfo);

        // Define o tipo da expressão lambda: Expression<Func<T>>
        var lambdaType = typeof(Func<>).MakeGenericType(targetType, propertyInfo.PropertyType);

        // Cria a expressão lambda com o tipo dinâmico Expression<Func<T>>
        var lambda = Expression.Lambda(lambdaType, property, parameter);

        return lambda;
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
                Console.WriteLine($"SetValue in Model {Model!.GetType()}, Value {value}, Property {propertyInfo.Name}");
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Failed to set value to the model");
            }
        }
    }
}