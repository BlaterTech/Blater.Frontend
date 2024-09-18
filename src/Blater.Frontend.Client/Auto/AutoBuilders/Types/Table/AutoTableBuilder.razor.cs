using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;
using Blater.Frontend.Client.Enumerations;
using Blater.Frontend.Client.Models.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
public partial class AutoTableBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseFrontendModel
{
    public AutoTableBuilder()
    {
        OnCreate = EventCallback.Factory.Create(this, _ => { NavigationService.NavigateTo($"{typeof(T).Name}/Create"); });

        OnDetails = EventCallback.Factory.Create<T>(this, item => { NavigationService.NavigateTo($"{typeof(T).Name}/Details/{item.Id}"); });

        OnEditChanged = EventCallback.Factory.Create<T>(this, item => { NavigationService.NavigateTo($"{typeof(T).Name}/Edit/{item.Id}"); });

        OnDisabledChanged = EventCallback.Factory.Create<T>(this, item =>
        {
            /*item.Enabled = !item.Enabled;
            var updated = await DataRepository.Update(item);
            if (updated.HandleErrors(out var errors, out _))
            {
                Snackbar.Add(LocalizationService.GetValue($"BlaterTable-OnDisabledChanged-{_typeName}-Error"), Severity.Error);
                Logger.LogError("Error === Event(OnDisabledChanged), Message: {Errors}", string.Join(", ", errors.Select(x => x.Message).ToList()));
                return;
            }*/

            Snackbar.Add(LocalizationService.GetValue($"BlaterTable-OnDisabledChanged-{_typeName}-Success"), Severity.Success);
        });

        OnCheckboxChanged = EventCallback.Factory.Create<(T model, bool value)>(this, item => { });
    }
    
    [Parameter]
    public List<T> Items { get; set; } = [];
    
    [Parameter]
    public bool Loading { get; set; }

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

    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Table;
    public override bool HasLabel { get; set; }
    private AutoTableConfiguration<T> TableConfiguration { get; set; } = default!;

    private List<IAutoTablePropertyConfiguration<T>> ColumnConfigurations
        => TableConfiguration
          .Configurations
          .Where(x => !x.DisableColumn)
          .ToList();

    private readonly string _typeName = typeof(T).Name;
    private MudTable<T> _mudTable = null!;
    private readonly int[] _qtdPageSize = [10, 25, 50, 100];

    private bool _open;
    private string _iconFilter = Icons.Material.Outlined.FilterAlt;
    private DateRange? _dateRange;

    protected override void LoadModelConfig()
    {
        var autoTable = FindModelConfig<IAutoTableConfiguration<T>>();
        TableConfiguration = autoTable.TableConfiguration;
    }
    
    private async Task DateRangeValueChanged(DateRange obj)
    {
        _dateRange = obj;
        await Task.Delay(1);
    }

    private static Func<T, object> CreateSortFunc(string propName)
    {
        var param = Expression.Parameter(typeof(T), "p");
        var body = Expression.Convert(Expression.Property(param, propName), typeof(object));
        return Expression.Lambda<Func<T, object>>(body, param).Compile();
    }

    private async Task Filter()
    {
        await Task.Delay(1);
        _iconFilter = Icons.Material.Filled.FilterAlt;
    }

    private void FilterOpen()
    {
        _open = !_open;

        if (_whereParts.Count == 0)
        {
            _whereParts.Add(new WherePart("", OperatorTypes.Contains, ""));
        }
    }

    public record WherePart(string Column, OperatorTypes Operator, object Value);

    private List<WherePart> _whereParts = [];

    private void RemoveWherePart(WherePart wherePart)
    {
        _whereParts.Remove(wherePart);
        StateHasChanged();
    }

    private void ClearWherePart()
    {
        _whereParts.Clear();
        StateHasChanged();
    }

    private void AddWherePart()
    {
        _whereParts.Add(new WherePart("", OperatorTypes.Contains, ""));
        StateHasChanged();
    }
}