using System.Reflection.Metadata;
using Blater.Frontend.Helpers;
using Blater.Frontend.Logging;

[assembly: MetadataUpdateHandler(typeof(HotReloadHelper))]

namespace Blater.Frontend.Helpers;

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