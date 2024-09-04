using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Details;
using Blater.Frontend.Client.Auto.Interfaces.Form;
using Blater.Frontend.Client.Auto.Interfaces.Table;
using Blater.Frontend.Client.Auto.Interfaces.Validator;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
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
                                .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IAutoConfiguration)))
                                .ToList();

        foreach (var modelType in models)
        {
            // Create instance of model type, and inject services
            var instance = ActivatorUtilities.CreateInstance(_serviceProvider, modelType);

            ConfigureGenericModel(instance, modelType, typeof(IAutoFormConfiguration));
            ConfigureGenericModel(instance, modelType, typeof(IAutoDetailsConfiguration));
            ConfigureGenericModel(instance, modelType, typeof(IAutoTableConfiguration));
            ConfigureGenericModel(instance, modelType, typeof(IAutoValidatorConfiguration<>));
            
            Configurations.Add(modelType, instance);
        }

        ModelsChanged?.Invoke();
    }

    void ConfigureGenericModel(object instance, Type modelType, Type configurationType)
    {
        if (!modelType.IsAssignableTo(configurationType)) return;

        var method = modelType.GetMethod("Configure");
        if (method == null) return;

        var builder = Activator.CreateInstance(configurationType.MakeGenericType(modelType));

        // Invoke the Configure method on the instance with the builder
        method.Invoke(instance, [builder]);
    }
}