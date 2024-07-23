using Blater.Frontend.StateManagement.Database;

namespace Blater.Frontend.Interfaces;

public interface IBlaterMemoryCache
{
    TimeSpan DefaultTimeout { get; set; }
    HashSet<string> Keys { get; }
    HashSet<CacheItem> Values { get; }
    Task<TValue?> Get<TValue>(string key);
    Task<object?> Get(string key);
    TValue GetOrSet<TValue>(string key, Func<TValue> valueFactory, TimeSpan? timeout = null);
    bool TryGetValue<TValue>(string key, out TValue value);
    void Set(string key, object? value, TimeSpan? timeout = null);
    void Remove(string key);
    void Remove(IEnumerable<string> keys);
    bool ContainsKey(string key);
    void Clear();
}