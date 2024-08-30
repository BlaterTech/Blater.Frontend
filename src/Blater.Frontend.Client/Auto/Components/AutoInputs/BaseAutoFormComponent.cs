using Blater.Frontend.Client.Auto.AutoModels.Base;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Components.AutoInputs;

public abstract class BaseAutoFormComponent<TValue> : BaseValueAutoComponent<TValue>
{
    [Parameter]
    public int MinImageWidth { get; set; }

    [Parameter]
    public int MinImageHeight { get; set; }

    [Parameter]
    public List<string>? ErrorMessages { get; set; }

    public string ValidationErrorText => string.Join("", ErrorMessages ?? []);

    public new bool HasValidationError => ErrorMessages != null && ErrorMessages.Count != 0;
    
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    ///     If True it means that the value has been changed by the user
    /// </summary>
    [Parameter]
    public bool Dirty { get; set; }
    
    protected async Task NotifyValueChanged()
    {
        await ValueChanged.InvokeAsync(Value);
        StateHasChanged();
    }

    protected async Task NotifyValueChanged(TValue value)
    {
        Dirty = true;
        Value = value;
        await NotifyValueChanged();
    }
}