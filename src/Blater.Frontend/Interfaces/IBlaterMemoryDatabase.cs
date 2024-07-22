namespace Blater.Frontend.Interfaces;

public interface IBlaterMemoryDatabase
{
    Task Insert(string key, object value);
    Task Insert(Type key, object value);

    Task Remove(string key);
    Task Remove(Type key);
    
    object? Get(Type key);
    object? Get(string key);

    Task Clear();
    Task LoadFromLocalStorage();
}