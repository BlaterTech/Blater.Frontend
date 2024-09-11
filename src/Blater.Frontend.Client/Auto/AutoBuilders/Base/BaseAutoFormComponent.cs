using System.Reflection;
using Blater.Frontend.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Base;

public abstract class BaseAutoFormComponent<TValue> : BaseAutoValueComponent<TValue>, IDisposable
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
    public EventCallback<TValue> OnValueChanged { get; set; }

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;

    /// <summary>
    ///  If True it means that the value has been changed by the user
    /// </summary>
    [Parameter]
    public bool Dirty { get; set; }

    protected override void OnInitialized()
    {
        StateNotifierService.StateChanged += OnStateChanged;
    }

    protected async Task NotifyValueChanged(TValue value)
    {
        Console.WriteLine("PropertyName => " + TypeName);
        Console.WriteLine("Value => " + value);
        Dirty = true;
        Value = value;
        await OnValueChanged.InvokeAsync(Value);
        StateHasChanged();
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
        StateNotifierService.StateChanged -= OnStateChanged;
        GC.SuppressFinalize(this);
    }
    
    private void OnStateChanged(PropertyInfo propertyInfo, object? value)
    {
        if (propertyInfo.Name == AutoComponentConfiguration.Property.Name && value != null)
        {
            Value = (TValue)value;
            Console.WriteLine("Value => " + value);
            StateHasChanged();
        }
    }
}