namespace Blater.Frontend.Interfaces;

public interface IBlaterLocalStorageService
{
    public string BaseKey { get; }
    
    ValueTask SetItem(string key, string item);
    ValueTask SetItem<T>(string key, T item) where T : class;

    ValueTask<string?> GetItem(string key);
    ValueTask<T?> GetItem<T>(string key);

    ValueTask RemoveItem(string key);
    ValueTask RemoveItem<T>();
}