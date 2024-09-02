using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Frontend.Client.Services;
using Blater.Helpers;

namespace Blater.Frontend.Client.Auto;

[SuppressMessage("Design", "CA1000:Não declarar membros estáticos em tipos genéricos")]
public static class AutoConfigurations<TModel>
{
    /// <summary>
    ///     Parent Type is the key, and the value is the configuration for the child type.
    /// </summary>
    public static readonly Dictionary<Type, AutoModelConfiguration> Configurations = new();
    
    static AutoConfigurations()
    {
        BuildAllConfigurations();

        HotReloadHelper.UpdateApplicationEvent += HotReloadHelperOnUpdateApplicationEvent;
    }

    public static event Action? ModelsChanged;

    private static void HotReloadHelperOnUpdateApplicationEvent(Type[]? obj)
    {
        BuildAllConfigurations();
    }

    public static void BuildAllConfigurations()
    {
        using var _ = new LogTimer("Building all model configurations");
        Configurations.Clear();

        var models = TypesHelper.AllTypes
                                .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IAutoConfiguration<TModel>)));

        foreach (var modelType in models)
        {
            var instance = Activator.CreateInstance(modelType) as IAutoConfiguration<TModel>;

            if (instance == null)
            {
                continue;
            }
            
            var configurationBuilder = new AutoModelConfigurationBuilder<TModel>(modelType);

            instance.Configure(configurationBuilder);
        }

        ModelsChanged?.Invoke();
    }
}