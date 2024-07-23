using Blater.Frontend.Interfaces;
using Blater.JsonUtilities;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.StateManagement.Database;

public class BlaterMemoryCache(ILocalStorageService localStorage) : IBlaterMemoryCache
{
    private readonly Dictionary<string, CacheItem> _cache = new();
    private readonly ReaderWriterLockSlim _cacheLock = new();
    private bool _isInitialized;
    private const string BaseKey = "blater-state";
    public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromMinutes(5.0);

    public HashSet<string> Keys
    {
        get
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _cache.Keys.ToHashSet();
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }
    }

    public HashSet<CacheItem> Values
    {
        get
        {
            _cacheLock.EnterReadLock();
            try
            {
                return _cache.Values.ToHashSet();
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }
    }

    public async Task<TValue?> Get<TValue>(string key)
    {
        await LoadValues();
        _cacheLock.EnterReadLock();
        try
        {
            key = $"{BaseKey}-{key.ToLower()}";
            if (_cache.TryGetValue(key, out var cacheItem) && !cacheItem.IsExpired())
            {
                return (TValue)cacheItem.Value;
            }
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }

        var storedValue = await localStorage.GetItemAsync<TValue>(key);
        if (storedValue == null) return default;

        Set(key, storedValue);
        return storedValue;
    }

    public Task<object?> Get(string key)
    {
        return Get<object>(key);
    }

    public TValue GetOrSet<TValue>(string key, Func<TValue> valueFactory, TimeSpan? timeout = null)
    {
        _cacheLock.EnterUpgradeableReadLock();
        try
        {
            key = $"{BaseKey}-{key.ToLower()}";
            if (_cache.TryGetValue(key, out var cacheItem) && !cacheItem.IsExpired())
            {
                return (TValue)cacheItem.Value;
            }

            var value = valueFactory();
            Set(key, value, timeout);
            return value;
        }
        finally
        {
            _cacheLock.ExitUpgradeableReadLock();
        }
    }

    public bool TryGetValue<TValue>(string key, out TValue value)
    {
        _cacheLock.EnterReadLock();
        try
        {
            key = $"{BaseKey}-{key.ToLower()}";
            if (_cache.TryGetValue(key, out var cacheItem) && !cacheItem.IsExpired())
            {
                value = (TValue)cacheItem.Value;
                return true;
            }

            value = default!;
            return false;
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }
    }

    public void Set(string key, object? value, TimeSpan? timeout = null)
    {
        if (value == null) return;

        _cacheLock.EnterWriteLock();
        try
        {
            var cacheItem = new CacheItem(value, DateTime.UtcNow.Add(timeout ?? DefaultTimeout));
            key = $"{BaseKey}-{key.ToLower()}";
            _cache[key] = cacheItem;
            localStorage.SetItemAsync(key, cacheItem);
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    public void Remove(string key)
    {
        _cacheLock.EnterWriteLock();
        try
        {
            key = $"{BaseKey}-{key.ToLower()}";
            if (_cache.Remove(key))
            {
                localStorage.RemoveItemAsync(key);
            }
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    public void Remove(IEnumerable<string> keys)
    {
        _cacheLock.EnterWriteLock();
        try
        {
            foreach (var key in keys)
            {
                var newKey = $"{BaseKey}-{key.ToLower()}";
                if (_cache.Remove(newKey))
                {
                    localStorage.RemoveItemAsync(newKey);
                }
            }
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    public bool ContainsKey(string key)
    {
        _cacheLock.EnterReadLock();
        try
        {
            key = $"{BaseKey}-{key.ToLower()}";
            return _cache.ContainsKey(key);
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }
    }

    public void Clear()
    {
        _cacheLock.EnterWriteLock();
        try
        {
            _cache.Clear();
            localStorage.ClearAsync();
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    private async Task LoadValues()
    {
        if (_isInitialized) return;

        var keys = await localStorage.KeysAsync();
        foreach (var key in keys)
        {
            var newKey = "";
            newKey = !key.Contains("blater-state") ? $"{BaseKey}-{key.ToLower()}" : key;

            var item = await localStorage.GetItemAsync<CacheItem>(newKey);
            if (item != null)
            {
                _cache[newKey] = item;
            }
        }

        _isInitialized = true;
    }
}