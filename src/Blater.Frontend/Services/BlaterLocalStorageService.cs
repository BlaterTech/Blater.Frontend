using Blater.Frontend.Interfaces;
using Blazored.LocalStorage;

namespace Blater.Frontend.Services;

public class BlaterLocalStorageService(ILocalStorageService localStorageService) : IBlaterLocalStorageService
{
    public string BaseKey => "Blater";

    public ValueTask SetItem(string key, string data)
        => localStorageService.SetItemAsStringAsync($"{BaseKey}-{key}", data);

    public ValueTask SetItem<T>(string key, T data) where T : class
        => localStorageService.SetItemAsync($"{BaseKey}-{key}", data);

    public ValueTask<string?> GetItem(string key)
        => localStorageService.GetItemAsStringAsync($"{BaseKey}-{key}");

    public ValueTask<T?> GetItem<T>(string key)
        => localStorageService.GetItemAsync<T>($"{BaseKey}-{key}");

    public ValueTask RemoveItem(string key)
        => localStorageService.RemoveItemAsync($"{BaseKey}-{key}");

    public ValueTask RemoveItem<T>()
        => localStorageService.RemoveItemAsync($"{BaseKey}-{typeof(T).Name}");
}