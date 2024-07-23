namespace Blater.Frontend.StateManagement.Database;

public class CacheItem(object value, DateTime expiration)
{
    public object Value { get; } = value;
    public DateTime Expiration { get; } = expiration;

    public bool IsExpired() => DateTime.UtcNow >= Expiration;
}