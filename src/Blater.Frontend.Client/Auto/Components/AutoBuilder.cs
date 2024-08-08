using Blater.Frontend.Client.Auto.Components.AutoTable.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;

namespace Blater.Frontend.Client.Auto.Components;

public static class AutoBuilder
{
    private static bool _hotReloadInitialized;
    private static readonly List<Type> GenericBuildableComponents = [];
    private static readonly Type BaseTableComponentType = typeof(ITableConfiguration<>);
    private static readonly Type BaseFormComponentType = typeof(ITableConfiguration<>);

    private static void InitializeHotReload()
    {
        if (_hotReloadInitialized)
        {
            return;
        }

        _hotReloadInitialized = true;
        HotReloadHelper.UpdateApplicationEvent += _ => Initialize();
    }
    
    private static List<Type> GetBuildableComponentTypes(Type baseComponentType)
    {
        return TypesHelper.AllTypes
                          .Where(x => baseComponentType.IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
                          .ToList();
    }

    public static void Initialize()
    {
        InitializeHotReload();

        using var _ = new LogTimer("AutoComponentsBuilders.Initialize");

        var buildableTableTypes = GetBuildableComponentTypes(BaseTableComponentType);
        var buildableFormTypes = GetBuildableComponentTypes(BaseFormComponentType);

        var buildableComponentsTypes = new List<Type>();
        buildableComponentsTypes.AddRange(buildableTableTypes);
        buildableComponentsTypes.AddRange(buildableFormTypes);

        //Get generic components
        foreach (var buildableComponentType in buildableComponentsTypes.Where(x => x.IsGenericType))
        {
            //Console.WriteLine(buildableComponentType);
            GenericBuildableComponents.Add(buildableComponentType);
        }
    }
}