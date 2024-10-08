﻿using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Routes;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Routes;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Routes;
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
        BuildAllValidators();
        BuildRouteConfigurations();

        HotReloadHelper.UpdateApplicationEvent += HotReloadHelperOnUpdateApplicationEvent;
    }

    /// <summary>
    ///     Parent Type is the key, and the value is the configuration for the child type.
    /// </summary>
    public Dictionary<Type, object> Configurations { get; set; } = new();
    
    public Dictionary<Type, object> Validators { get; set; } = new();

    public AutoRouteConfiguration Route { get; set; } = new();

    public static event Action? ModelsChanged;

    private void HotReloadHelperOnUpdateApplicationEvent(Type[]? obj)
    {
        BuildAllConfigurations();
        BuildAllValidators();
        BuildRouteConfigurations();
    }

    private void BuildAllValidators()
    {
        using var _ = new LogTimer("Building all model validators");
        Validators.Clear();
        
        var validationModels = TypesHelper.AllTypes
                                          .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IBaseAutoValidator)))
                                          .ToList();

        foreach (var validatorType in validationModels)
        {
            var modelType = validatorType.BaseType?.GetGenericArguments()[0];
            
            if (modelType == null)
            {
                throw new Exception($"Not found GenericArgument in {validatorType.Name}");
            }
            
            var instance = ActivatorUtilities.CreateInstance(_serviceProvider, validatorType);
            
            Validators.Add(modelType, instance);
        }
        
        ModelsChanged?.Invoke();
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

            ConfigureGenericModel(instance, modelType, typeof(IAutoFormConfiguration<>), typeof(AutoFormConfigurationBuilder<>));
            ConfigureGenericModel(instance, modelType, typeof(IAutoFormTimelineConfiguration<>), typeof(AutoFormTimelineConfigurationBuilder<>));
            ConfigureGenericModel(instance, modelType, typeof(IAutoDetailsConfiguration<>), typeof(AutoDetailsConfigurationBuilder<>));
            ConfigureGenericModel(instance, modelType, typeof(IAutoDetailsTabsConfiguration<>), typeof(AutoDetailsTabsConfigurationBuilder<>));
            ConfigureGenericModel(instance, modelType, typeof(IAutoTableConfiguration<>), typeof(AutoTableConfigurationBuilder<>));

            Configurations.Add(modelType, instance);
        }

        ModelsChanged?.Invoke();
    }

    private void BuildRouteConfigurations()
    {
        using var _ = new LogTimer("Building route configurations");

        Route = new AutoRouteConfiguration();
        
        var model = TypesHelper.AllTypes
                                .FirstOrDefault(x => x is
                                 {
                                     IsInterface: false
                                 } && x.IsAssignableTo(typeof(IAutoRouteConfiguration)));

        if (model == null)
        {
            Log.Error("Not found route configuration");
            return;
        }
        
        var instance = Activator.CreateInstance(model) as IAutoRouteConfiguration;
        var builder = new AutoRouteConfigurationBuilder(Route);
        instance!.ConfigureRoute(builder);
        
        ModelsChanged?.Invoke();
    }

    private static void ConfigureGenericModel(object instance, Type modelType, Type configurationType, Type builderType)
    {
        var genericInterface = configurationType.MakeGenericType(modelType);
        if (!modelType.IsAssignableTo(genericInterface))
        {
            Log.Information("ModelType {ModelType} is not AssignableTo {ConfigurationType}", modelType.Name, configurationType.Name);
            return;
        }

        var configurationName = configurationType
                               .Name
                               .Replace("IAuto", "")
                               .Replace("Configuration", "")
                               .Replace("`1", "");
        var methodName = $"Configure{configurationName}";
        var method = modelType.GetMethod(methodName);
        if (method == null) return;

        var genericBuilderType = builderType.MakeGenericType(modelType);
        var builder = Activator.CreateInstance(genericBuilderType, instance);

        method.Invoke(instance, [builder]);
    }
}