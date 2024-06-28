using System.Reflection;
using Serilog;

namespace Blater.Frontend.Helpers;

public static class TypesHelper
{
    static TypesHelper()
    {
        Initialize();
    }
    
    public static HashSet<Assembly> Assemblies { get; } = new();
    
    public static HashSet<Type> AllTypes { get; } = new();
    
    public static Dictionary<string, Type> BaseDataModels { get; } = new();
    
    public static void Initialize()
    {
        try
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (var assembly in assemblies)
            {
                try
                {
                    if (!assembly.GetName().Name?.StartsWith("Blater") ?? false)
                        //Log.Debug("Skipping assembly {AssemblyName}", assembly.FullName);
                    {
                        continue;
                    }
                    
                    //Log.Debug("Adding assembly {AssemblyName}", assembly.FullName);
                    Assemblies.Add(assembly);
                    AllTypes.UnionWith(assembly.GetTypes());
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error during TypesHelper initialization for assembly {AssemblyName}", assembly.FullName);
                }
            }
        }
        catch (Exception e)
        {
            Log.Error(e, "Error during TypesHelper initialization");
        }
    }
    
    /*public static void InitializeBaseDataModels()
    {
        try
        {
            var baseDataModels = AllTypes.Where(x => x.IsSubclassOf(typeof(BaseDataModel))).ToList();
            
            foreach (var baseDataModel in baseDataModels)
            {
                var name = baseDataModel.Name;
                BaseDataModels.Add(name, baseDataModel);
            }
        }
        catch (Exception e)
        {
            Log.Error(e, "Error during TypesHelper.InitializeBaseDataModels");
        }
    }*/
    
    public static Type GetTypeFromName(this string typeName)
    {
        var type = AllTypes.FirstOrDefault(x => x.Name == typeName);
        
        if (type == null)
        {
            throw new Exception($"Type {typeName} not found");
        }
        
        return type;
    }
}