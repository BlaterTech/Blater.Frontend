﻿using System.Reflection;
using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Types.Details;
using Blater.Frontend.Client.Auto.Interfaces.Types.Form;
using Blater.Frontend.Client.Auto.Interfaces.Types.Table;
using Blater.Frontend.Client.Auto.Interfaces.Types.Validator;
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

    public void BuildAllConfigurations()
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
            ConfigureModel(instance, modelType, typeof(IAutoDetailsConfiguration), typeof(AutoDetailsConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoTableConfiguration), typeof(AutoTableConfigurationBuilder));
            ConfigureGenericModel(instance, modelType, typeof(IAutoValidatorConfiguration<>), typeof(AutoValidatorBuilder<>));

            Configurations.Add(modelType, instance);
        }

        ModelsChanged?.Invoke();
    }

    private void ConfigureModel(object instance, Type modelType, Type configurationType, Type builderType)
    {
        if (!modelType.IsAssignableTo(configurationType))
        {
            Log.Information("ModelType {ModelType} is not AssignableTo {ConfigurationType}", modelType.Name, configurationType.Name);
            return;
        }

        var method = modelType.GetMethod("Configure", [builderType]);
        if (method == null)
        {
            Log.Information("Method Configure not found in BuilderType {BuilderType}", builderType.Name);
            throw new InvalidOperationException($"Method Configure not found in BuilderType {builderType.Name}");
        }

        var builder = Activator.CreateInstance(builderType, modelType, instance);
        
        method.Invoke(instance, [builder]);
    }
    
    private void ConfigureGenericModel(object instance, Type modelType, Type configurationType, Type builderType)
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