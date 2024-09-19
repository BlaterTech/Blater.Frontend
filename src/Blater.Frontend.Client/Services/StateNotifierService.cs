using System.Linq.Expressions;
using System.Reflection;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoExtensions;

namespace Blater.Frontend.Client.Services;

public static class StateNotifierService
{
    public static event Action<PropertyInfo, object>? StateChanged;
    
    public static void NotifyStateHasChanged(PropertyInfo propertyInfo, object value)
    {
        Console.WriteLine($"Start Notify State Has Changed, target type {propertyInfo.Name}");
        StateChanged?.Invoke(propertyInfo, value);
    }
}