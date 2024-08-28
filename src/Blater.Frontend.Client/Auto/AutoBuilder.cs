using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;

namespace Blater.Frontend.Client.Auto;

public static class AutoBuilder
{
    private static bool _hotReloadInitialized;
    private static readonly Dictionary<BaseAutoComponentTypeEnumeration, IAutoBuildableComponent> BuildableComponentsDictionary = new();
    private static readonly List<Type> GenericBuildableComponents = [];
    private static readonly Type BaseComponentType = typeof(IAutoConfiguration);

    private static void InitializeHotReload()
    {
        if (_hotReloadInitialized)
        {
            return;
        }

        _hotReloadInitialized = true;
        HotReloadHelper.UpdateApplicationEvent += _ => Initialize();
    }

    public static void Initialize()
    {
        InitializeHotReload();

        using var _ = new LogTimer("AutoComponentsBuilders.Initialize");

        var buildableComponentTypes = TypesHelper.AllTypes
                                             .Where(x => BaseComponentType.IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
                                             .ToList();
        
        //Simple components
        foreach (var buildableComponentType in buildableComponentTypes.Where(x => !x.IsGenericType))
        {
            //Console.WriteLine(buildableComponentType);
            var buildableComponent = (IAutoBuildableComponent)Activator.CreateInstance(buildableComponentType)!;
            BuildableComponentsDictionary.TryAdd(buildableComponent.ComponentType, buildableComponent);
        }
        
        //Get generic components
        foreach (var buildableComponentType in buildableComponentTypes.Where(x => x.IsGenericType))
        {
            //Console.WriteLine(buildableComponentType);
            GenericBuildableComponents.Add(buildableComponentType);
        }
    }
}