using System.Linq.Expressions;
using Blater.Frontend.StateManagement;

namespace Blater.Frontend.Interfaces;

public interface IBlaterStateStore
{
    void AddSubscription<T>(IStateComponent aBlazorStateComponent);
    void AddSubscription(Type aType, IStateComponent aBlazorStateComponent);
    Task SetState(object? state);
    Task<T> GetState<T>(); 
    Task<object> GetState(Type type);
    void DeleteState<T>();
    void DeleteState(Type type);
    void Subscribe<T>(Action callback);
    bool Equals(object? aObject);
    int GetHashCode();
    void Remove(IStateComponent aBlazorStateComponent);
    Task ReRenderSubscribers<T>();
    Task<T?> WaitForState<T>(Expression<Func<T, bool>> predicate);
}