using Blater.Frontend.Client.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Linq.Expressions;
using System.Reflection;

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

    [Parameter]
    public object? ModelInstance { get; set; }

    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;

    /// <summary>
    ///  If True it means that the value has been changed by the user
    /// </summary>
    [Parameter]
    public bool Dirty { get; set; }

    protected Expression<Func<TValue>>? For { get; set; }

    protected override void OnInitialized()
    {
        StateNotifierService.StateChanged += OnStateChanged;

        For = CreateExpression();
    }

    protected async Task NotifyValueChanged(TValue value)
    {
        Console.WriteLine("PropertyName => " + TypeName);
        Console.WriteLine("Value NotifyValueChanged => " + value);
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
        if (propertyInfo.Name != AutoPropertyConfiguration.Property.Name || value == null)
        {
            return;
        }

        Value = (TValue)value;
        Console.WriteLine("Value OnStateChanged => " + value);
        StateHasChanged();
    }

    private Expression<Func<TValue>> CreateExpression()
    {
        if (ModelInstance == null)
        {
            throw new Exception("Model instance is nullable");
        }

        var targetType = ModelInstance.GetType();

        var property = targetType.GetProperty(AutoPropertyConfiguration.Property.Name);

        if (property == null)
        {
            throw new ArgumentException($"Propriedade {AutoPropertyConfiguration.Property.Name} não encontrada em {targetType.Name}");
        }

        var instanceExpression = Expression.Constant(ModelInstance);

        var propertyAccess = Expression.Property(instanceExpression, property);

        var lambda = Expression.Lambda<Func<TValue>>(propertyAccess, []);

        return lambda;
    }
}