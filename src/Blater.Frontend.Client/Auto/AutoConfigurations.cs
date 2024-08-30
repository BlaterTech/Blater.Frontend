using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;

namespace Blater.Frontend.Client.Auto;

public static class AutoConfigurations
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
                                .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IAutoConfiguration)));

        foreach (var modelType in models)
        {
            var instance = Activator.CreateInstance(modelType) as IAutoConfiguration;

            if (instance == null)
            {
                continue;
            }
            
            var configurationBuilder = new AutoModelConfigurationBuilder(modelType);

            instance.Configure(configurationBuilder);
        }

        ModelsChanged?.Invoke();
    }
}