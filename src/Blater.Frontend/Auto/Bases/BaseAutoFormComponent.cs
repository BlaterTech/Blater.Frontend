using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blater.Frontend.Auto.Bases;

public abstract class BaseAutoFormComponent<TValue> : BaseValueAutoComponent<TValue>
{
    [Parameter]
    public int MinImageWidth { get; set; }

    [Parameter]
    public int MinImageHeight { get; set; }

    [Parameter]
    public List<string>? ErrorMessages { get; set; }

    public string ValidationErrorSummary => string.Join("", ErrorMessages ?? new List<string>());

    public new bool HasValidationError => ErrorMessages != null && ErrorMessages.Count != 0;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }
    
    
    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;
    
    public Guid InputId { get; set; } = Guid.NewGuid();

    /// <summary>
    ///  If True it means that the value has been changed by the user
    /// </summary>
    [Parameter]
    public bool Dirty { get; set; }

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
            if (Mask!= null)
            {
                await JsRuntime.InvokeVoidAsync("applyMask", InputId, Mask);
            }
        }
        return;
    }
}