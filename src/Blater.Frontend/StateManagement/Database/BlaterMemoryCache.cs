using Blater.Frontend.Extensions;
using Blater.Frontend.Interfaces;
using Blazored.LocalStorage;

namespace Blater.Frontend.StateManagement.Database;

public class BlaterMemoryCache(ILocalStorageService localStorage) : IBlaterMemoryCache
{
    private readonly Dictionary<string, CacheItem> _cache = new();
    private readonly ReaderWriterLockSlim _cacheLock = new();
    private bool _loading;
    
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
        _cacheLock.EnterReadLock();
        key = key.SanitizeKey();
        await LoadValues();
        try
        {
            if (_cache.TryGetValue(key, out var cacheItem) && !cacheItem.IsExpired())
            {
                return (TValue)cacheItem.Value;
            }

            return default;
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }
    }

    public Task<object?> Get(string key)
    {
        return Get<object>(key);
    }

    public async Task<TValue> GetOrSet<TValue>(string key, Func<TValue> valueFactory, TimeSpan? timeout = null)
    {
        _cacheLock.EnterUpgradeableReadLock();
        await LoadValues();
        try
        {
            key = key.SanitizeKey();
            if (_cache.TryGetValue(key, out var cacheItem) && !cacheItem.IsExpired())
            {
                return (TValue)cacheItem.Value;
            }

            var value = valueFactory();
            await Set(key, value, timeout);
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
            key = key.SanitizeKey();
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

    public async Task Set(string key, object? value, TimeSpan? timeout = null)
    {
        if (value == null) return;

        _cacheLock.EnterWriteLock();
        await LoadValues();
        try
        {
            var cacheItem = new CacheItem(value, DateTime.UtcNow.Add(timeout ?? DefaultTimeout));
            key = key.SanitizeKey();
            _cache[key] = cacheItem;
            await localStorage.SetItemAsync(key, cacheItem);
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    public async Task Remove(string key)
    {
        _cacheLock.EnterWriteLock();
        await LoadValues();
        try
        {
            key = key.SanitizeKey();
            if (_cache.Remove(key))
            {
                await localStorage.RemoveItemAsync(key);
            }
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    public async Task Remove(IEnumerable<string> keys)
    {
        _cacheLock.EnterWriteLock();
        await LoadValues();
        try
        {
            foreach (var key in keys)
            {
                var newKey = key.SanitizeKey();
                if (_cache.Remove(newKey))
                {
                    await localStorage.RemoveItemAsync(newKey);
                }
            }
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    public async Task<bool> ContainsKey(string key)
    {
        _cacheLock.EnterReadLock();
        await LoadValues();
        try
        {
            key = key.SanitizeKey();
            return _cache.ContainsKey(key);
        }
        finally
        {
            _cacheLock.ExitReadLock();
        }
    }

    public async Task Clear()
    {
        _cacheLock.EnterWriteLock();
        await LoadValues();
        try
        {
            _cache.Clear();
            await localStorage.ClearAsync();
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }

    private async Task LoadValues()
    {
        if (_loading)
        {
            return;
        }
        
        var keys = await localStorage.KeysAsync();

        foreach (var key in keys)
        {
            var newKey = key.SanitizeKey();

            var item = await localStorage.GetItemAsync<CacheItem>(newKey);
            if (item != null)
            {
                _cache[newKey] = item;
            }
        }

        _loading = true;
    }
}