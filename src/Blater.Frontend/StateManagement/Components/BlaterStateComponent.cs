using Blater.Frontend.Interfaces;
using Blater.JsonUtilities;
using Blater.Utilities;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.StateManagement.Components;

public class BlaterStateComponent : ComponentBase, IStateComponent, IDisposable
{
    public string ComponentStateId => SequentialGuidGenerator.NewGuid().ToString().Replace("-", "")[..10];
    
    [Inject]
    public IBlaterStateStore StateStoreService { get; set; } = null!;
    
    [Inject]
    public IBlaterMemoryCache MemoryCache { get; set; } = null!;
    
    public async Task ReRender()
    {
        await InvokeAsync(StateHasChanged);
    }

    protected async Task<T> GetState<T>()
    {
        var stateType = typeof(T);
        StateStoreService.AddSubscription(stateType, this);
        var result = await StateStoreService.GetState<T>();
        return result;
    }

    protected async Task SetState<T>(T state)
    {
        await StateStoreService.SetState(state);
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}