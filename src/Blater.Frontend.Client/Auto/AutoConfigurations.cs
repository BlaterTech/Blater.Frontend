using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Valitador;
using Blater.Frontend.Client.Auto.AutoInterfaces;
using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Blater.Frontend.Client.Auto;

public class AutoConfigurations
{
    private readonly IServiceProvider _serviceProvider;

    public AutoConfigurations(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        BuildAllConfigurations();

        HotReloadHelper.UpdateApplicationEvent += HotReloadHelperOnUpdateApplicationEvent;
    }

    /// <summary>
    ///     Parent Type is the key, and the value is the configuration for the child type.
    /// </summary>
    public Dictionary<Type, object> Configurations { get; } = new();

    public static event Action? ModelsChanged;

    private void HotReloadHelperOnUpdateApplicationEvent(Type[]? obj)
    {
        BuildAllConfigurations();
    }

    private void BuildAllConfigurations()
    {
        using var _ = new LogTimer("Building all model configurations");
        Configurations.Clear();

        var models = TypesHelper.AllTypes
                                .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IBaseAutoConfiguration)))
                                .ToList();

        foreach (var modelType in models)
        {
            var instance = ActivatorUtilities.CreateInstance(_serviceProvider, modelType);

            ConfigureModel(instance, modelType, typeof(IAutoFormConfiguration), typeof(AutoFormConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoFormTimelineConfiguration), typeof(AutoFormTimelineConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoDetailsConfiguration), typeof(AutoDetailsConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoDetailsTabsConfiguration), typeof(AutoDetailsTabsConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoTableConfiguration), typeof(AutoTableConfigurationBuilder));
            ConfigureGenericModel(instance, modelType, typeof(IAutoValidatorConfiguration<>), typeof(AutoValidatorConfigurationBuilder<>));

            Configurations.Add(modelType, instance);
        }

        ModelsChanged?.Invoke();
    }

    private static void ConfigureModel(object instance, Type modelType, Type configurationType, Type builderType)
    {
        if (!modelType.IsAssignableTo(configurationType))
        {
            Log.Information("ModelType {ModelType} is not AssignableTo {ConfigurationType}", modelType.Name, configurationType.Name);
            return;
        }

        var method = modelType.GetMethods().FirstOrDefault(x => x.Name.StartsWith("Configure"));
        if (method == null)
        {
            Log.Information("Method Configure not found in BuilderType {BuilderType}", builderType.Name);
            throw new InvalidOperationException($"Method Configure not found in BuilderType {builderType.Name}");
        }

        var builder = Activator.CreateInstance(builderType, modelType, instance);
        
        method.Invoke(instance, [builder]);
    }
    
    private static void ConfigureGenericModel(object instance, Type modelType, Type configurationType, Type builderType)
    {
        var genericInterface = configurationType.MakeGenericType(modelType);
        if (!modelType.IsAssignableTo(genericInterface))
        {
            Log.Information("ModelType {ModelType} is not AssignableTo {ConfigurationType}", modelType.Name, configurationType.Name);
            return;
        }

        var genericBuilderType = builderType.MakeGenericType(modelType);
        var method = modelType.GetMethod("Configure", [genericBuilderType]);
        if (method == null)
        {
            Log.Information("Method Configure not found in BuilderType {BuilderType}", builderType.Name);
            throw new InvalidOperationException($"Method Configure not found in BuilderType {builderType.Name}");
        }
        
        var builder = Activator.CreateInstance(genericBuilderType, instance);
        
        method.Invoke(instance, [builder]);
    }
}