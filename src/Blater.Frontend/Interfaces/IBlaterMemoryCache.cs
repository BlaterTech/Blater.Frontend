using Blater.Frontend.StateManagement.Database;

namespace Blater.Frontend.Interfaces;

public interface IBlaterMemoryCache
{
    TimeSpan DefaultTimeout { get; set; }
    HashSet<string> Keys { get; }
    HashSet<CacheItem> Values { get; }
    Task<TValue?> Get<TValue>(string key);
    Task<object?> Get(string key);
    Task<TValue> GetOrSet<TValue>(string key, Func<TValue> valueFactory, TimeSpan? timeout = null);
    bool TryGetValue<TValue>(string key, out TValue value);
    Task Set(string key, object? value, TimeSpan? timeout = null);
    Task Remove(string key);
    Task Remove(IEnumerable<string> keys);
    Task<bool> ContainsKey(string key);
    Task Clear();
}