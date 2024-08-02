using System.Reflection.Metadata;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Logging;

[assembly: MetadataUpdateHandler(typeof(HotReloadHelper))]

namespace Blater.Frontend.Client.Helpers;

public static class HotReloadHelper
{
    public static event Action<Type[]?>? UpdateApplicationEvent;
    
    internal static void ClearCache(Type[]? types)
    {
    }
    
    internal static void UpdateApplication(Type[]? types)
    {
        using var _ = new LogTimer("HotReloadHelper.UpdateApplication");
        UpdateApplicationEvent?.Invoke(types);
    }
}