using System.Diagnostics.CodeAnalysis;
using Blater.AutoModelConfigurations.Configs;
using Blater.Enumerations.AutoModel;
using Blater.Helpers;
using Blater.Logging;
using Serilog;

namespace Blater.FrontEnd.Auto;

/// <summary>
///     Finder of types that can be built using the auto form builder
/// </summary>
[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public static class AutoComponentsBuilders
{
    private static readonly Type BaseComponentType = typeof(IAutoBuildableComponent);

    private static readonly Dictionary<BaseAutoComponentTypeEnumeration, IAutoBuildableComponent> BuildableComponentsDictionary = new();
    private static readonly List<Type> GenericBuildableComponents = [];


    private static bool _hotReloadInitialized;

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
        BuildableComponentsDictionary.Clear();

        var buildableComponentsTypes = TypesHelper.AllTypes
                                                  .Where(x => BaseComponentType.IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false }).ToList();

        //Simple components
        foreach (var buildableComponentType in buildableComponentsTypes.Where(x => !x.IsGenericType))
        {
            //Console.WriteLine(buildableComponentType);
            var buildableComponent = (IAutoBuildableComponent)Activator.CreateInstance(buildableComponentType)!;
            BuildableComponentsDictionary.TryAdd(buildableComponent.ComponentType, buildableComponent);
        }

        //Get generic components
        foreach (var buildableComponentType in buildableComponentsTypes.Where(x => x.IsGenericType))
        {
            //Console.WriteLine(buildableComponentType);
            GenericBuildableComponents.Add(buildableComponentType);
        }
    }

    public static IAutoBuildableComponent? GetComponentBuilder(AutoComponentConfiguration componentConfiguration, AutoComponentDisplayType displayType)
    {
        var fieldConfigurationDisplayType = componentConfiguration.ComponentTypes.GetValueOrDefault(displayType);

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
        var genericComponent = GenericBuildableComponents.FirstOrDefault(x => x.Name.StartsWith(fieldConfigurationDisplayType.Name));

        if (genericComponent == null)
        {
            Log.Error("No component builder found for {ComponentName}", fieldConfigurationDisplayType.Name);
            return null;
        }

        //Generic Component
        if (componentConfiguration.Property is not null)
        {
            //If property is list pass the TItem and TList type to create the generic type
            if (componentConfiguration.Property.PropertyType.IsGenericType && componentConfiguration.Property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var itemType = componentConfiguration.Property.PropertyType.GetGenericArguments().First();

                var listGenericType = genericComponent.MakeGenericType(itemType, componentConfiguration.Property.PropertyType);

                return Activator.CreateInstance(listGenericType) as IAutoBuildableComponent;
            }

            //Normal generic component
            var genericType = genericComponent.MakeGenericType(componentConfiguration.Property.PropertyType);

            return Activator.CreateInstance(genericType) as IAutoBuildableComponent;
        }

        return null;
    }
}