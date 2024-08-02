using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models;
using Blater.Interfaces;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Blater.Frontend.Client.Components;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
[SuppressMessage("Usage", "CA2254:O modelo deve ser uma expressão estática")]
public partial class BlaterTable<T> : ComponentBase where T : BaseDataModel
{
    #region Constructor

    public BlaterTable()
    {
        OnCreate = EventCallback.Factory.Create(this, _ => { NavigationService.NavigateTo($"{typeof(T).Name}/Create"); });

        OnDetails = EventCallback.Factory.Create<T>(this, item => { NavigationService.NavigateTo($"{typeof(T).Name}/Details/{item.Id}"); });

        OnEditChanged = EventCallback.Factory.Create<T>(this, item => { NavigationService.NavigateTo($"{typeof(T).Name}/Edit/{item.Id}"); });

        OnDisabledChanged = EventCallback.Factory.Create<T>(this, async item =>
        {
            item.Enabled = !item.Enabled;
            var updated = await DatabaseStoreT.Update(item);
            if (updated.HandleErrors(out var errors, out _))
            {
                Snackbar.Add(LocalizationService.GetValue($"BlaterTable-OnDisabledChanged-{_typeName}-Error"), Severity.Error);
                Logger.LogError($"Error === Event(OnDisabledChanged), Message: {string.Join(", ", errors.Select(x => x.Message).ToList())}");
                return;
            }

            Snackbar.Add(LocalizationService.GetValue($"BlaterTable-OnDisabledChanged-{_typeName}-Success"), Severity.Success);
        });

        OnCheckboxChanged = EventCallback.Factory.Create<(T model, bool value)>(this, item => { });
    }

    #endregion
    
    #region Parameters

    #region Events

    [Parameter]
    public EventCallback<T> OnDisabledChanged { get; set; }

    [Parameter]
    public EventCallback<T> OnDetails { get; set; }

    [Parameter]
    public EventCallback<T> OnEditChanged { get; set; }

    [Parameter]
    public EventCallback<T> OnDeleteChanged { get; set; }

    [Parameter]
    public EventCallback<T> OnAddChanged { get; set; }
    
    [Parameter]
    public EventCallback<T> OnFilterChanged { get; set; }

    [Parameter]
    public EventCallback<(T model, bool value)> OnCheckboxChanged { get; set; }

    [Parameter]
    public EventCallback OnCreate { get; set; }
    
    [Parameter]
    public EventCallback<HashSet<T>> OnSelectedItemsChanged { get; set; }
    
    [Parameter]
    public EventCallback<T> OnSelectedItemChanged { get; set; }
    
    [Parameter]
    public EventCallback<TableRowClickEventArgs<T>> OnRowClick { get; set; }

    #endregion

    #region Props

    [Parameter]
    public List<T> Items { get; set; } = [];

    [Parameter]
    public List<Guid> Ids { get; set; } = [];
    
    [Parameter]
    public bool CreateButton { get; set; } = true;

    [Parameter]
    public bool ShowDefaultActions { get; set; } = true;
    
    [Parameter]
    public bool ShowCustomActions { get; set; }
    
    [Parameter]
    public List<CustomDataGridAction<T>> CustomDataGridActions { get; set; } = [];

    [Parameter]
    public bool EnabledMultiSelection { get; set; }
    
    [Parameter]
    public bool EnabledSelectionChangeable { get; set; }    
    
    [Parameter]
    public bool EnabledSelectOnRowClick { get; set; }

    [Parameter]
    public bool EnabledFixedHeader { get; set; } = true;
    
    [Parameter]
    public bool EnabledFixedFooter { get; set; }
    
    [Parameter]
    public bool EnabledStriped { get; set; } = true;

    [Parameter]
    public Color LoadingProgressColor { get; set; } = Color.Info;

    #endregion

    #endregion

    #region Injects

    [Inject]
    public ILogger<BlaterTable<T>> Logger { get; set; } = null!;

    [Inject]
    public IBlaterDatabaseStoreT<T> DatabaseStoreT { get; set; } = null!;

    [Inject]
    public ILocalizationService LocalizationService { get; set; } = null!;

    [Inject]
    public INavigationService NavigationService { get; set; } = null!;
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion
    
    private MudTable<T> _mudTable = null!;
    private bool _loading;
    private DateRange? _dateRange;
    private string _typeName = typeof(T).Name;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        
        await Task.Delay(1500);

        _loading = false;
    }

    private async Task DateRangeValueChanged(DateRange obj)
    {
        _dateRange = obj;
        await Task.Delay(1);
    }
    
    private static async Task Filter()
    {
        await Task.Delay(1);
    }
    
    private static Func<T, object> CreateSortFunc(string propName)
    {
        var param = Expression.Parameter(typeof(T), "p");
        var body = Expression.Convert(Expression.Property(param, propName), typeof(object));
        return Expression.Lambda<Func<T, object>>(body, param).Compile();
    }

    private void QueryFilter(object? value, string propertyName)
    {
        try
        {
            if (value == null || string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }
            
            var type = value.GetType();
            string stringValue;
            if (type == typeof(string))
            {
                stringValue = value.ToString()!;
            }
            else
            {
                var defaultValue = Activator.CreateInstance(type);

                if (value.Equals(defaultValue))
                {
                    return;
                }

                stringValue = value.ToString()!;
            }
            
            var prop = typeof(T).GetProperty(propertyName);
            if (prop == null)
            {
                return;
            }

            Items = Items
                   .Where(e => prop.GetValue(e)?.ToString()?.Contains(stringValue, StringComparison.OrdinalIgnoreCase) == true)
                   .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            StateHasChanged();
        }
    }
}