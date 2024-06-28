using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Blater.AutoModelConfigurations;
using Blater.AutoModelConfigurations.Configs;
using Blater.Enumerations.AutoModel;
using Blater.Frontend.Services;
using Blater.Models;
using Blater.Models.Bases;
using Blater.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Blater.Frontend.Auto;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public partial class AutoDataGrid<T> where T : BaseDataModel
{
    private List<AutoComponentConfiguration> _componentConfigurations = null!;

    private DateRange? _dateRange;

    private bool _loading = true;
    private string _typeName = typeof(T).Name;

    public AutoDataGrid()
    {
        CreateCallBack = EventCallback.Factory.Create(this, _ => { NavigationService.Navigate($"{typeof(T).Name}/Create"); });

        DetailsCallback = EventCallback.Factory.Create<T>(this, item => { NavigationService.Navigate($"{typeof(T).Name}/Details/{item.Id}"); });

        EditCallback = EventCallback.Factory.Create<T>(this, item => { NavigationService.Navigate($"{typeof(T).Name}/Edit/{item.Id}"); });

        DisableCallback = EventCallback.Factory.Create<T>(this, item =>
        {
            item.Enabled = !item.Enabled;
            DataRepository.Update(item.Id, item);
        });

        CheckboxCallback = EventCallback.Factory.Create<(T model, bool value)>(this, _ => { });
    }


    [Parameter]
    public EventCallback<T> DisableCallback { get; set; }

    [Parameter]
    public EventCallback<T> DetailsCallback { get; set; }

    [Parameter]
    public EventCallback<T> EditCallback { get; set; }

    [Parameter]
    public EventCallback<T> DeleteCallback { get; set; }

    [Parameter]
    public EventCallback<T> AddCallback { get; set; }

    [Parameter]
    public EventCallback<(T model, bool value)> CheckboxCallback { get; set; }

    [Parameter]
    public EventCallback CreateCallBack { get; set; }
    
    [Parameter]
    public EventCallback<HashSet<T>> SelectedItemsCallback { get; set; }

    [Parameter]
    public List<T> Items { get; set; } = [];

    [Parameter]
    public List<BlaterId> Ids { get; set; } = [];

    [Parameter]
    public bool CreateButton { get; set; } = true;

    [Parameter]
    public bool ShowDefaultActions { get; set; } = true;
    
    [Parameter]
    public bool ShowCustomActions { get; set; } = false;
    
    [Parameter]
    public List<CustomDataGridAction<T>> CustomDataGridActions { get; set; } = [];

    [Parameter]
    public bool MultiSelection { get; set; } = false;
    
    public string Title { get; set; } = string.Empty;

    [Inject]
    public ILogger<AutoDataGrid<T>> Logger { get; set; } = null!;

    private AutoModelConfiguration ModelConfiguration { get; set; } = null!;

    public bool IsMobile { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        LocalizationService.LocalizationChanged += UpdateDataGrid;
        ModelConfigurations.ModelsChanged += UpdateDataGrid;

        void UpdateDataGrid()
        {
            UpdateModelConfiguration();
            InvokeAsync(StateHasChanged);
        }

        if (string.IsNullOrEmpty(Title))
        {
            Title = LocalizationService.Get(typeof(T).Name);
        }

        UpdateModelConfiguration();
        
        BlaterResult<IReadOnlyList<T>>? items;
        
        if (Ids.Count > 0)
        {
            items = await DataRepository.FindMany(x => Ids.Contains(x.Id));
        }
        else
        {
            items = await DataRepository.FindMany(x => x.CreatedAt != DateTimeOffset.MinValue);
        }
        
        if (items.Value is null)
        {
            Items = [];
            Logger.LogWarning("No items found for model {ModelName}", typeof(T).Name);
            return;
        }

        Items = items.Value.ToList();
        
        _loading = false;
    }

    private void UpdateModelConfiguration()
    {
        var modelConfiguration = ModelConfigurations.AutoConfigurations.GetValueOrDefault(typeof(T));

        if (modelConfiguration is null)
        {
            Logger.LogWarning("No configuration found for model {ModelName} in the FieldConfigurations", typeof(T).Name);
            return;
        }


        ModelConfiguration = modelConfiguration;

        _componentConfigurations = modelConfiguration.ComponentConfigurations
                                                     .Where(x => x.ComponentTypes.GetValueOrDefault(AutoComponentDisplayType.DataGrid) != null && x.Property != null)
                                                     .OrderBy(x => x.Order[AutoComponentDisplayType.DataGrid])
                                                     .ToList();
    }

    private string _search = string.Empty;

    private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", [typeof(string), typeof(StringComparison)])!;
    //private static readonly MethodInfo ToStringMethod = typeof(object).GetMethod("ToString")!;

    private async Task SearchChanged(string obj)
    {
        _search = obj;
        await Filter();
    }

    private async Task DateRangeChanged(DateRange obj)
    {
        _dateRange = obj;

        await Filter();
    }

    private async Task Filter()
    {
        // Build the expression to filter the items on all properties
        var parameter = Expression.Parameter(typeof(T), "x");

        Expression? combined = null;

        foreach (var component in _componentConfigurations)
        {
            var property = Expression.Property(parameter, component.Property!.Name);

            if (property.Type != typeof(string))
            {
                continue;
            }

            //var toStringCall = Expression.Call(property, ToStringMethod);
            var constant = Expression.Constant(_search);
            var contains = Expression.Call(property, ContainsMethod, constant, Expression.Constant(StringComparison.OrdinalIgnoreCase));

            combined = combined == null ? contains : Expression.OrElse(combined, contains);
        }

        //Add the date range filter
        if (_dateRange != null && (_dateRange.Start != null || _dateRange.End != null))
        {
            var dateProperty = Expression.Property(parameter, "CreatedAt");
            var dateStart = Expression.Constant(new DateTimeOffset(_dateRange.Start ?? DateTime.UtcNow, TimeSpan.Zero));
            var dateEnd = Expression.Constant(new DateTimeOffset(_dateRange.End     ?? DateTime.UtcNow, TimeSpan.Zero));
            var dateFromExpression = Expression.GreaterThanOrEqual(dateProperty, dateStart);
            var dateToExpression = Expression.LessThanOrEqual(dateProperty, dateEnd);
            var dateCombined = Expression.AndAlso(dateFromExpression, dateToExpression);
            combined = combined == null ? dateCombined : Expression.AndAlso(combined, dateCombined);
        }

        if (combined == null)
        {
            var allItems = await DataRepository.FindMany(x => x.CreatedAt != DateTimeOffset.MinValue);
            Items = allItems.Value?.ToList() ?? [];
            return;
        }

        var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);

        var items = await DataRepository.FindMany(lambda);

        Items = items.Value?.ToList() ?? [];
    }

    private async Task HandleSelectedItemsChanged(HashSet<T> obj)
    {
        await SelectedItemsCallback.InvokeAsync(obj);
    }
}