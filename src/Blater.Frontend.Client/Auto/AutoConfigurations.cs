using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;
using Blater.Helpers;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto;

[SuppressMessage("Design", "CA1000:Não declarar membros estáticos em tipos genéricos")]
public static class AutoConfigurations<T, TConfiguration>
    where T : BaseDataModel
    where TConfiguration : AutoModels.BaseConfiguration
{
    /// <summary>
    ///     Parent Type is the key, and the value is the configuration for the child type.
    /// </summary>
    public static readonly Dictionary<Type, TConfiguration> Configurations = new();

    /// <summary>
    ///     Parent Type is the key, and the value is the configuration for the child type.
    /// </summary>
    //public static readonly Dictionary<Type, TableConfiguration<T>> Configurations = new();
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
                                .Where(x => x is { IsInterface: false, IsAbstract: false } && x.IsAssignableTo(typeof(IAutoConfiguration<T>)));

        foreach (var modelType in models)
        {
            var instance = Activator.CreateInstance(modelType) as IAutoConfiguration<T>;

            if (instance == null)
            {
                continue;
            }

            var tableConfiguration = new TableConfiguration();
            var tableConfigurator = new AutoTableConfigurationBuilder<T>(tableConfiguration);

            var formConfiguration = new FormConfiguration<T>();
            var formConfigurator = new AutoFormConfigurationBuilder<T>(formConfiguration);

            //var detailsConfiguration = new FormConfiguration<T>();
            //var detailsConfigurator = new AutoFormConfigurationBuilder<T>(detailsConfiguration);

            instance.Configure(tableConfigurator);
            instance.Configure(formConfigurator);
            //instance.Configure(detailsConfigurator);
        }

        ModelsChanged?.Invoke();
    }
}