using Blater.Frontend.Client.Auto.AutoTable.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;

namespace Blater.Frontend.Client.Auto.AutoTable;

public static class AutoTableBuilder
{
    private static bool _hotReloadInitialized;
    private static readonly List<Type> GenericBuildableComponents = [];
    private static readonly Type BaseComponentType = typeof(ITableConfiguration<>);
    
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

        var buildableComponentsTypes = TypesHelper.AllTypes
                                                  .Where(x => BaseComponentType.IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false }).ToList();

        //Get generic components
        foreach (var buildableComponentType in buildableComponentsTypes.Where(x => x.IsGenericType))
        {
            //Console.WriteLine(buildableComponentType);
            GenericBuildableComponents.Add(buildableComponentType);
        }
    }
}