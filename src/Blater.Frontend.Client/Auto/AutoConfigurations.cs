using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;
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
            Console.WriteLine($"Configuration model {modelType.Name}");
            // Create instance of model type, and inject services
            var instance = ActivatorUtilities.CreateInstance(_serviceProvider, modelType);

            ConfigureModel(instance, modelType, typeof(IAutoFormConfiguration), typeof(AutoFormConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoDetailsConfiguration), typeof(AutoDetailsConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoTableConfiguration), typeof(AutoTableConfigurationBuilder));
            ConfigureModel(instance, modelType, typeof(IAutoValidatorConfiguration<>), typeof(AutoValidatorBuilder<>), true);

            Configurations.Add(modelType, instance);
        }

        ModelsChanged?.Invoke();
    }

    private static void ConfigureModel(object instance, Type modelType, Type configurationType, Type builderType, bool isGenericType = false)
    {
        if (!modelType.IsAssignableTo(configurationType)) return;

        var method = modelType.GetMethod("Configure", [builderType]);
        if (method == null) return;

        object? builder;
        if (isGenericType)
        {
            // Cria uma instância do tipo genérico do builder
            var genericBuilderType = builderType.MakeGenericType(modelType);
            builder = Activator.CreateInstance(genericBuilderType, modelType, instance);
        }
        else
        {
            // Cria uma instância do builder com parâmetros
            builder = Activator.CreateInstance(builderType, modelType, instance);
        }
        // Invoke the Configure method on the instance with the builder
        method.Invoke(instance, [builder]);
    }
}