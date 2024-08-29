using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;
using Serilog;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public static class AutoComponentsBuilders
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
    
    public static IAutoBuildableComponent? GetComponentBuilder(BasePropertyConfiguration configuration, AutoComponentDisplayType displayType)
    {
        var fieldConfigurationDisplayType = configuration.AutoComponentTypes.GetValueOrDefault(displayType);

        if (fieldConfigurationDisplayType == null)
        {
            Log.Error("No display type found for {ComponentName}", displayType);
            return null;
        }
        
        //Simple component
        if (BuildableComponentsDictionary.TryGetValue(fieldConfigurationDisplayType, out var buildableComponent))
        {
            return buildableComponent;
        }

        //Check if it is a generic component
        var genericComponent = GenericBuildableComponents.FirstOrDefault(x => x.Name.StartsWith(fieldConfigurationDisplayType.Name, StringComparison.OrdinalIgnoreCase));

        if (genericComponent == null)
        {
            Log.Error("No component builder found for {ComponentName}", fieldConfigurationDisplayType.Name);
            return null;
        }

        //Generic Component
        //If property is list pass the TItem and TList type to create the generic type
        if (configuration.Property.PropertyType.IsGenericType && configuration.Property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
        {
            var itemType = configuration.Property.PropertyType.GetGenericArguments().First();

            var listGenericType = genericComponent.MakeGenericType(itemType, configuration.Property.PropertyType);

            return Activator.CreateInstance(listGenericType) as IAutoBuildableComponent;
        }

        //Normal generic component
        var genericType = genericComponent.MakeGenericType(configuration.Property.PropertyType);

        return Activator.CreateInstance(genericType) as IAutoBuildableComponent;
    }
}