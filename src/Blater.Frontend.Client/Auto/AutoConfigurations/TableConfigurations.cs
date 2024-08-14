﻿using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces.AutoTable;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoConfigurations;

[SuppressMessage("Design", "CA1000:Não declarar membros estáticos em tipos genéricos")]
public static class TableConfigurations<T> where T : BaseDataModel
{
    /// <summary>
    ///     Parent Type is the key, and the value is the configuration for the child type.
    /// </summary>
    public static readonly Dictionary<Type, TableConfiguration<T>> Configurations = new();
    
    static TableConfigurations()
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
                                .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IAutoTableConfiguration<T>)));
        
        foreach (var modelType in models)
        {
            Console.WriteLine("modelType =>" + modelType.Name);
            var instance = Activator.CreateInstance(modelType) as IAutoTableConfiguration<T>;

            if (instance == null)
            {
                continue;
            }

            var tableConfiguration = new TableConfiguration<T>();
            
            var configurator = new AutoTableConfigurationBuilder<T>(tableConfiguration);
            instance.Configure(configurator);
        }
        
        ModelsChanged?.Invoke();
    }
}