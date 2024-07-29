using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using Blater.Frontend.Interfaces;
using Blater.JsonUtilities;
using Blater.Models.User;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.StateManagement;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
public class BlaterStateStore(IBlaterMemoryCache memoryCache, ILogger<BlaterStateStore> logger)
    : IBlaterStateStore, IDisposable
{
    private record Subscription(Type StateType, string ComponentId, WeakReference<IStateComponent?> WeakReference);

    private readonly List<Subscription> _blazorStateComponentReferencesList = [];

    private readonly Dictionary<Type, List<Action>> _subscriptions = new();

    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public void AddSubscription<T>(IStateComponent aBlazorStateComponent)
    {
        var type = typeof(T);
        AddSubscription(type, aBlazorStateComponent);
    }

    public void AddSubscription(Type aType, IStateComponent aBlazorStateComponent)
    {
        // Add only once.
        if (_blazorStateComponentReferencesList.Any(aSubscription =>
                aSubscription.StateType == aType &&
                aSubscription.ComponentId == aBlazorStateComponent.ComponentStateId)) return;

        var subscription = new Subscription(
            aType,
            aBlazorStateComponent.ComponentStateId,
            new WeakReference<IStateComponent?>(aBlazorStateComponent));

        _blazorStateComponentReferencesList.Add(subscription);
    }

    public async Task SetState(object? state, TimeSpan? timeout = null)
    {
        if (state == null) return;

        await _semaphoreSlim.WaitAsync();
        try
        {
            var type = state.GetType();
            var name = type.Name;
            await memoryCache.Set(name, state, timeout);
            await ReRenderSubscribers(type);
            NotifyStateChanged(type);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task<T> GetState<T>()
    {
        var type = typeof(T);
        var result = await GetState(type);

        var value = result switch
        {
            JsonElement jsonElement => jsonElement.GetRawText().FromJson<T>() ?? Activator.CreateInstance<T>(),
            T typedValue => typedValue,
            _ => Activator.CreateInstance<T>()
        };
        
        return value;
    }

    public async Task<object> GetState(Type type)
    {
        var key = type.Name;
        var value = await memoryCache.Get(key);
        if (value != null)
        {
            return value;
        }
        
        try
        {
            var emptyState = Activator.CreateInstance(type);

            if (emptyState == null)
                throw new InvalidOperationException($"Could not create instance of type {type}");

            await memoryCache.Set(key, emptyState);
            return emptyState;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not create instance of type {Type}", type);
        }

        throw new InvalidOperationException($"Could not find or create instance of type {type}");
    }

    public void DeleteState<T>()
    {
        var type = typeof(T);
        DeleteState(type);
    }

    public void DeleteState(Type type)
    {
        var name = type.Name;
        memoryCache.Remove(name);
    }

    public void Subscribe<T>(Action callback)
    {
        var type = typeof(T);
        if (_subscriptions.TryGetValue(type, out var value))
        {
            value.Add(callback);
        }
        else
        {
            _subscriptions.Add(type, [callback]);
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is BlaterStateStore service &&
               EqualityComparer<List<Subscription>>.Default.Equals(
                   _blazorStateComponentReferencesList,
                   service._blazorStateComponentReferencesList);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(_blazorStateComponentReferencesList);
        return hash.ToHashCode();
    }


    public void Remove(IStateComponent aBlazorStateComponent)
    {
        _blazorStateComponentReferencesList.RemoveAll(aRecord =>
            aRecord.ComponentId == aBlazorStateComponent.ComponentStateId);
    }

    /// <summary>
    ///     Will iterate over all subscriptions for the given type and call ReRender on each.
    ///     If the target component no longer exists it will remove its subscription.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public async Task ReRenderSubscribers<T>()
    {
        var type = typeof(T);

        await ReRenderSubscribers(type);
    }

    public async Task<T?> WaitForState<T>(Expression<Func<T, bool>> predicate)
    {
        var compiled = predicate.Compile();
        var attempts = 0;
        while (attempts < 10)
        {
            var state = await GetState<T>();
            if (state != null && compiled(state))
            {
                return state;
            }

            attempts++;
        }

        logger.LogError("Could not get expected state after {Attempts} attempts", attempts);
        return default;
    }

    /// <summary>
    ///     Will iterate over all subscriptions for the given type and call ReRender on each.
    ///     If the target component no longer exists it will remove its subscription.
    /// </summary>
    /// <param name="aType"></param>
    private async Task ReRenderSubscribers(Type aType)
    {
        // If the state is BlaterUserToken, we need to re-render all components, because all components depend on it.
        if (aType == typeof(BlaterUserToken))
        {
            foreach (var subscription in _blazorStateComponentReferencesList.ToList())
            {
                if (subscription.WeakReference.TryGetTarget(out var target))
                {
                    await target.ReRender();
                }
                else
                {
                    _blazorStateComponentReferencesList.Remove(subscription);
                }
            }

            return;
        }

        var subscriptions = _blazorStateComponentReferencesList.Where(aRecord => aRecord.StateType == aType);

        foreach (var subscription in subscriptions.ToList())
        {
            if (subscription.WeakReference.TryGetTarget(out var target))
            {
                await target.ReRender();
            }
            else
            {
                _blazorStateComponentReferencesList.Remove(subscription);
            }
        }
    }


    private void NotifyStateChanged(Type type)
    {
        if (!_subscriptions.TryGetValue(type, out var value)) return;
        foreach (var action in value)
        {
            action();
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}