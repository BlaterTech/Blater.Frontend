using System.Globalization;
using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces.Types.Table;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Table;
    public override bool HasLabel { get; set; }
    private AutoTableConfiguration TableConfiguration { get; set; } = default!;

    private DateRange? _dateRange;
    private readonly string _typeName = typeof(T).Name;
    private readonly MudTable<T> _mudTable = null!;
    private bool _open;
    private string _iconFilter = Icons.Material.Outlined.FilterAlt;

    protected override void LoadModelConfig()
    {
        var autoTable = FindModelConfig<IAutoTableConfiguration>();
        TableConfiguration = autoTable.TableConfiguration;
    }

    protected override void BuildComponent(EasyRenderTreeBuilder builder)
    {
        builder
           .OpenComponent<MudCard>()
           .AddChildContent(cardBuilder =>
            {
                BuildFilterComponent(cardBuilder);
                BuildTableComponent(cardBuilder);
            })
           .Close();
    }

    private void BuildFilterComponent(EasyRenderTreeBuilder builder)
    {
        builder
           .OpenElement("form")
           .AddChildContent(formGridBuilder =>
            {
                formGridBuilder
                   .OpenComponent<MudGrid>()
                   .AddChildContent(formGridContentBuilder =>
                    {
                        formGridContentBuilder
                           .OpenComponent<MudItem>()
                           .AddAttribute(nameof(MudItem.xs), 12)
                           .AddChildContent(treeBuilder =>
                            {
                                treeBuilder
                                   .OpenComponent<MudText>()
                                   .AddAttribute(nameof(MudText.Typo), Typo.h5)
                                   .AddChildContent(renderTreeBuilder =>
                                    {
                                        renderTreeBuilder
                                           .AddContent(LocalizationService.GetValue($"BlaterTable-{_typeName}-Filter"));
                                    })
                                   .Close();
                            })
                           .Close();
                        formGridContentBuilder
                           .OpenComponent<MudItem>()
                           .AddAttribute(nameof(MudItem.xs), 12)
                           .AddAttribute(nameof(MudItem.lg), 8)
                           .AddChildContent(treeBuilder =>
                            {
                                treeBuilder
                                   .OpenComponent<MudDateRangePicker>()
                                   .AddAttribute(nameof(MudDateRangePicker.Variant), Variant.Filled)
                                   .AddAttribute(nameof(MudDateRangePicker.Clearable), true)
                                   .AddAttribute(nameof(MudDateRangePicker.Culture), new CultureInfo("pt-BR"))
                                   .AddAttribute(nameof(MudDateRangePicker.Label), "BlaterTable-Date-Range-Label")
                                   .AddAttribute(nameof(MudDateRangePicker.DateRangeChanged), EventCallback.Factory.Create<DateRange>(this, DateRangeValueChanged))
                                   .AddAttribute(nameof(MudDateRangePicker.DateRange), _dateRange)
                                   .Close();
                            })
                           .Close();
                        formGridContentBuilder
                           .OpenComponent<MudItem>()
                           .AddAttribute(nameof(MudItem.xs), 12)
                           .AddAttribute(nameof(MudItem.lg), 4)
                           .AddChildContent(treeBuilder =>
                            {
                                treeBuilder
                                   .OpenComponent<MudDateRangePicker>()
                                   .AddAttribute(nameof(MudDateRangePicker.Variant), Variant.Filled)
                                   .AddAttribute(nameof(MudDateRangePicker.Clearable), true)
                                   .AddAttribute(nameof(MudDateRangePicker.Culture), new CultureInfo("pt-BR"))
                                   .AddAttribute(nameof(MudDateRangePicker.Label), "BlaterTable-Date-Range-Label")
                                   .AddAttribute(nameof(MudDateRangePicker.DateRangeChanged), EventCallback.Factory.Create<DateRange>(this, DateRangeValueChanged))
                                   .AddAttribute(nameof(MudDateRangePicker.DateRange), _dateRange)
                                   .Close();
                            })
                           .Close();
                    })
                   .Close();
            })
           .Close();
    }

    private void BuildTableComponent(EasyRenderTreeBuilder builder)
    {
        builder
           .OpenComponent<MudTable<T>>()
           .AddAttribute("ref", _mudTable)
           .AddAttribute(nameof(MudTable<T>.Items),)
           .AddAttribute(nameof(MudTable<T>.Striped),)
           .AddAttribute(nameof(MudTable<T>.MultiSelection),)
           .AddAttribute(nameof(MudTable<T>.SelectOnRowClick),)
           .AddAttribute(nameof(MudTable<T>.SelectionChangeable),)
           .AddAttribute(nameof(MudTable<T>.SelectedItemsChanged),)
           .AddAttribute(nameof(MudTable<T>.SelectedItemChanged),)
           .AddAttribute(nameof(MudTable<T>.OnRowClick),)
           .AddAttribute(nameof(MudTable<T>.FixedHeader),)
           .AddAttribute(nameof(MudTable<T>.FixedFooter),)
           .AddAttribute(nameof(MudTable<T>.Height),)
           .AddAttribute(nameof(MudTable<T>.Loading),)
           .AddAttribute(nameof(MudTable<T>.LoadingProgressColor),)
           .AddAttribute(nameof(MudTable<T>.HeaderContent), BuildHeaderContent(builder))
           .AddAttribute(nameof(MudTable<T>.RowTemplate), BuildRowContent(builder))
           .AddAttribute(nameof(MudTable<T>.PagerContent), BuildPagerContent(builder))
           .Close();
    }

    private RenderFragment BuildHeaderContent(EasyRenderTreeBuilder builder)
    {
        var columns = TableConfiguration.Configurations
                                        .Where(x => !x.DisableColumn)
                                        .ToList();
        foreach (var column in columns)
        {
            builder
               .OpenComponent<MudTh>()
               .AddChildContent(treeBuilder =>
                {
                    treeBuilder
                       .OpenElement("div")
                       .AddAttribute("class", "d-flex align-center justify-center")
                       .AddChildContent(renderTreeBuilder =>
                        {
                            renderTreeBuilder
                               .OpenComponent<MudTableSortLabel<T>>()
                               .AddAttribute(nameof(MudTableSortLabel<T>.SortBy), CreateSortFunc(column.Property.Name))
                               .AddAttribute(nameof(MudTableSortLabel<T>.Enabled), column.DisableColumn)
                               .AddChildContent(easyRenderTreeBuilder =>
                                {
                                    easyRenderTreeBuilder.AddContent("");
                                })
                               .Close();
                            renderTreeBuilder
                               .OpenComponent<MudIconButton>()
                               .AddAttribute(nameof(MudIconButton.Icon), _iconFilter)
                               .AddAttribute(nameof(MudIconButton.OnClick), EventCallback.Factory.Create<EventArgs>(this, ToggleOpen))
                               .Close();
                        })
                       .Close();
                })
               .Close();
        }

        if (true | true)
        {
            builder
               .OpenComponent<MudTh>()
               .Close();
        }

        return treeBuilder => treeBuilder.AddContent(0, builder);
    }

    private RenderFragment BuildRowContent(EasyRenderTreeBuilder builder)
    {
        return treeBuilder => treeBuilder.AddContent(0, builder);
    }

    private RenderFragment BuildPagerContent(EasyRenderTreeBuilder builder)
    {
        return treeBuilder => treeBuilder.AddContent(0, builder);
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

    private void ToggleOpen()
    {
        _open = !_open;
        
        /*if (_whereParts.Count == 0)
        {
            _whereParts.Add(new WherePart("", OperatorTypes.Contains, ""));
        }*/
    }
}