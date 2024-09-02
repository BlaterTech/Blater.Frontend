using Blater.Frontend.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseAutoFormComponent<TValue> : BaseValueAutoComponent<TValue>, IDisposable
{
    [Parameter]
    public int MinImageWidth { get; set; }

    [Parameter]
    public int MinImageHeight { get; set; }

    [Parameter]
    public List<string>? ErrorMessages { get; set; }

    public string ValidationErrorSummary => string.Join("", ErrorMessages ?? []);

    public new bool HasValidationError => ErrorMessages != null && ErrorMessages.Count != 0;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }
    
    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;

    [Inject]
    protected StateNotifierService StateNotifierService { get; set; } = default!;

    /// <summary>
    ///  If True it means that the value has been changed by the user
    /// </summary>
    [Parameter]
    public bool Dirty { get; set; }

    protected override void OnInitialized()
    {
        StateNotifierService.StateChanged += StateHasChanged;
    }

    public async Task NotifyValueChanged()
    {
        await ValueChanged.InvokeAsync(Value);
        StateHasChanged();
    }

    public async Task NotifyValueChanged(TValue value)
    {
        Dirty = true;
        Value = value;
        await NotifyValueChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Mask != null)
            {
                await JsRuntime.InvokeVoidAsync("applyMask", InputId, Mask);
            }
        }
    }

    public void Dispose()
    {
        StateNotifierService.StateChanged -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}