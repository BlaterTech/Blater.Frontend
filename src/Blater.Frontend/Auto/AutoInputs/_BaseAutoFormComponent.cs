using Blater.Frontend.Auto.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto.AutoInputs;

public abstract class _BaseAutoFormComponent<TValue> : BaseValueAutoComponent<TValue>
{
    [Parameter]
    public int MinImageWidth { get; set; }

    [Parameter]
    public int MinImageHeight { get; set; }

    [Parameter]
    public List<string>? ErrorMessages { get; set; }

    public string ValidationErrorText => string.Join("", ErrorMessages ?? new List<string>());

    public new bool HasValidationError => ErrorMessages != null && ErrorMessages.Count != 0;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    ///     If True it means that the value has been changed by the user
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
}