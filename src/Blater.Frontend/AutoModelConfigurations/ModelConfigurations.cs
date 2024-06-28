using Blater.Frontend.AutoModelConfigurations.Configs;
using Blater.Frontend.AutoModelConfigurations.Interfaces;
using Blater.Frontend.Helpers;
using Blater.Frontend.Logging;

namespace Blater.Frontend.AutoModelConfigurations;

public static class ModelConfigurations
{
    /// <summary>
    ///     Parent Type is the key, and the value is the configuration for the child type.
    /// </summary>
    public static readonly Dictionary<Type, AutoModelConfiguration> Configurations = new();
    
    static ModelConfigurations()
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
        
        var models = TypesHelper.AllTypes.Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IDataModelConfigurator)));
        
        foreach (var modelType in models)
        {
            //Console.WriteLine($"Building configuration for {modelType.Name}");
            var instance = Activator.CreateInstance(modelType) as IDataModelConfigurator;
            var configurator = new AutoModelConfigurator(modelType);
            instance?.Configure(configurator);
        }
        
        ModelsChanged?.Invoke();
    }
}