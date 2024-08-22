using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;

namespace Blater.Frontend.Client.Auto;

public static class AutoBuilder
{
    internal static bool HotReloadInitialized;
    internal static readonly List<Type> GenericBuildableComponents = [];
    internal static readonly Type BaseComponentType = typeof(IAutoConfiguration<>);

    private static void InitializeHotReload()
    {
        if (HotReloadInitialized)
        {
            return;
        }

        HotReloadInitialized = true;
        HotReloadHelper.UpdateApplicationEvent += _ => Initialize();
    }

    public static void Initialize()
    {
        InitializeHotReload();

        using var _ = new LogTimer("AutoComponentsBuilders.Initialize");

        var buildableComponentTypes = TypesHelper.AllTypes
                                             .Where(x => BaseComponentType.IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
                                             .ToList();

        //Get generic components
        foreach (var buildableComponentType in buildableComponentTypes.Where(x => x.IsGenericType))
        {
            //Console.WriteLine(buildableComponentType);
            GenericBuildableComponents.Add(buildableComponentType);
        }
    }
}