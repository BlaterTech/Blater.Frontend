using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Types;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Frontend.Client.Services;
using Blater.Helpers;
using Microsoft.Extensions.DependencyInjection;

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
                                .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IAutoConfiguration)));

        foreach (var modelType in models)
        {
            // Create instance of model type, and inject services
            var instance = ActivatorUtilities.CreateInstance(_serviceProvider, modelType);
            
            // Check if model type has interface IAutoForm<T>
            if (modelType.IsAssignableTo(typeof(IAutoForm<>)))
            {
                //Get ConfigureForm method and invoke it
                var method = modelType.GetMethod("ConfigureForm");
                //Create instance of AutoModelConfigurationBuilder<T> using reflection and modelType
                var builder = Activator.CreateInstance(typeof(AutoModelConfigurationBuilder<>).MakeGenericType(modelType));
                //Invoke method with instance and builder
                method?.Invoke(instance, [builder]);
            }
            
            /*if (modelType.IsAssignableTo(typeof(IAutoForm<>)))
            {
                //Get ConfigureForm method and invoke it
                var method = modelType.GetMethod("ConfigureForm");
                //Create instance of AutoModelConfigurationBuilder<T> using reflection and modelType
                var builder = Activator.CreateInstance(typeof(AutoModelConfigurationBuilder<>).MakeGenericType(modelType));
                //Invoke method with instance and builder
                method?.Invoke(instance, [builder]);
            }*/
            
            Configurations.Add(modelType, instance);
        }

        ModelsChanged?.Invoke();
    }
}