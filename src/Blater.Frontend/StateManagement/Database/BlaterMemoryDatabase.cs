using Blater.Frontend.Interfaces;
using Blater.JsonUtilities;
using Blazored.LocalStorage;

namespace Blater.Frontend.StateManagement.Database;

public class BlaterMemoryDatabase(ILocalStorageService localStorage) : IBlaterMemoryDatabase
{
    private readonly Dictionary<string, object> _store = new();

    public async Task Insert(string key, object value)
    {
        _store[key] = value;
        await SaveToLocalStorage(key, value);
    }

    public async Task Insert(Type key, object value)
    {
        var keyName = key.Name;
        await Insert(keyName, value);
    }

    public async Task Remove(string key)
    {
        var result = _store.Remove(key);

        if (result)
        {
            await localStorage.RemoveItemAsync(key);
        }
    }

    public async Task Remove(Type key)
    {
        var keyName = key.Name;
        await Remove(keyName);
    }

    public object? Get(Type key)
    {
        var keyName = key.Name;
        return Get(keyName);
    }

    public object? Get(string key)
    {
        var result = _store.GetValueOrDefault(key);
        Console.WriteLine("Get: " + result);
        return result;
    }

    public async Task Clear()
    {
        _store.Clear();
        await localStorage.ClearAsync();
    }
    
    //todo: pensar melhor em como carregar as informações do localStorage para a memory
    public async Task LoadFromLocalStorage()
    {
        var items = await localStorage.KeysAsync();
        var keys = items.ToList();
        foreach (var key in keys)
        {
            var result = await localStorage.GetItemAsStringAsync(key);
            if (string.IsNullOrWhiteSpace(result))
            {
                continue;
            }

            _store[key] = result;
        }
    }

    private async Task SaveToLocalStorage(string key, object value)
    {
        await localStorage.SetItemAsync(key, value);
    }
}