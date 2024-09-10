﻿using System.Globalization;
using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces.Types.Table;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    public AutoTableBuilder()
    {
        OnCreate = EventCallback.Factory.Create(this, _ => { NavigationService.NavigateTo($"{typeof(T).Name}/Create"); });

        OnDetails = EventCallback.Factory.Create<T>(this, item => { NavigationService.NavigateTo($"{typeof(T).Name}/Details/{item.Id}"); });

        OnEditChanged = EventCallback.Factory.Create<T>(this, item => { NavigationService.NavigateTo($"{typeof(T).Name}/Edit/{item.Id}"); });

        OnDisabledChanged = EventCallback.Factory.Create<T>(this, async item =>
        {
            item.Enabled = !item.Enabled;
            var updated = await DataRepository.Update(item);
            if (updated.HandleErrors(out var errors, out _))
            {
                Snackbar.Add(LocalizationService.GetValue($"BlaterTable-OnDisabledChanged-{_typeName}-Error"), Severity.Error);
                Logger.LogError("Error === Event(OnDisabledChanged), Message: {Errors}", string.Join(", ", errors.Select(x => x.Message).ToList()));
                return;
            }

            Snackbar.Add(LocalizationService.GetValue($"BlaterTable-OnDisabledChanged-{_typeName}-Success"), Severity.Success);
        });

        OnCheckboxChanged = EventCallback.Factory.Create<(T model, bool value)>(this, item => { });
    }

    [Parameter]
    public List<T> Items { get; set; } = [];

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
    private AutoTableConfiguration TableConfiguration { get; set; } = default!;

    private List<AutoTableAutoComponentConfiguration> ColumnConfigurations
        => TableConfiguration
          .Configurations
          .Where(x => !x.DisableColumn)
          .ToList();

    private readonly string _typeName = typeof(T).Name;
    private readonly MudTable<T> _mudTable = null!;
    private readonly int[] _qtdPageSize = [10, 25, 50, 100];
    
    private bool _open;
    private string _iconFilter = Icons.Material.Outlined.FilterAlt;
    private DateRange? _dateRange;

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
           .AddAttribute(nameof(MudTable<T>.Items), Items)
           .AddAttribute(nameof(MudTable<T>.SelectedItemsChanged), OnSelectedItemsChanged)
           .AddAttribute(nameof(MudTable<T>.SelectedItemChanged), OnSelectedItemChanged)
           .AddAttribute(nameof(MudTable<T>.OnRowClick), OnRowClick)
           .AddAttribute(nameof(MudTable<T>.Striped), TableConfiguration.EnabledStriped)
           .AddAttribute(nameof(MudTable<T>.MultiSelection), TableConfiguration.EnableMultiSelection)
           .AddAttribute(nameof(MudTable<T>.SelectOnRowClick), TableConfiguration.EnabledSelectOnRowClick)
           .AddAttribute(nameof(MudTable<T>.SelectionChangeable), TableConfiguration.EnabledSelectionChangeable)
           .AddAttribute(nameof(MudTable<T>.Loading), TableConfiguration.Loading)
           .AddAttribute(nameof(MudTable<T>.FixedHeader), TableConfiguration.EnableFixedFooter)
           .AddAttribute(nameof(MudTable<T>.FixedFooter), TableConfiguration.EnableFixedFooter)
           .AddAttribute(nameof(MudTable<T>.Height), TableConfiguration.EnableFixedFooter || TableConfiguration.EnableFixedHeader ? "70vh" : "")
           .AddAttribute(nameof(MudTable<T>.LoadingProgressColor), TableConfiguration.LoadingProgressColor)
           .AddAttribute(nameof(MudTable<T>.HeaderContent), BuildHeaderContent(builder))
           .AddAttribute(nameof(MudTable<T>.RowTemplate), (RenderFragment<T>)(context => BuildRowContent(builder, context)))
           .AddAttribute(nameof(MudTable<T>.PagerContent), BuildPagerContent(builder))
           .Close();
    }

    private RenderFragment BuildHeaderContent(EasyRenderTreeBuilder builder)
    {
        foreach (var column in ColumnConfigurations)
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
                               .AddChildContent(easyRenderTreeBuilder => { easyRenderTreeBuilder.AddContent(""); })
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

        if (TableConfiguration.EnableDefaultAction || TableConfiguration.EnableCustomAction)
        {
            builder
               .OpenComponent<MudTh>()
               .Close();
        }

        return treeBuilder => treeBuilder.AddContent(0, builder);
    }

    private RenderFragment BuildRowContent(EasyRenderTreeBuilder builder, T context)
    {
        foreach (var column in ColumnConfigurations)
        {
            builder
               .OpenComponent<MudTd>()
               .AddAttribute(nameof(MudTd.DataLabel), column.LabelName)
               .AddChildContent(treeBuilder =>
                {
                    var value = column.Property.GetValue(context);
                    treeBuilder
                       .AddContent(value);
                })
               .Close();
        }

        if (TableConfiguration.EnableDefaultAction || TableConfiguration.EnableCustomAction)
        {
            builder
               .OpenComponent<MudTd>()
               .AddChildContent(treeBuilder =>
                {
                    treeBuilder
                       .OpenElement("div")
                       .AddAttribute("class", "d-flex align-center justify-center gap-2")
                       .AddChildContent(renderTreeBuilder =>
                        {
                            if (TableConfiguration.EnableDefaultAction)
                            {
                                renderTreeBuilder
                                   .OpenComponent<MudIconButton>()
                                   .AddAttribute(nameof(MudIconButton.Icon), Icons.Material.Outlined.RemoveRedEye)
                                   .AddAttribute(nameof(MudIconButton.Color), Color.Primary)
                                   .AddAttribute(nameof(MudIconButton.OnClick), OnDetails.InvokeAsync(context))
                                   .Close();

                                renderTreeBuilder
                                   .OpenComponent<MudIconButton>()
                                   .AddAttribute(nameof(MudIconButton.Icon), Icons.Material.Outlined.Edit)
                                   .AddAttribute(nameof(MudIconButton.Color), Color.Primary)
                                   .AddAttribute(nameof(MudIconButton.OnClick), OnEditChanged.InvokeAsync(context))
                                   .Close();

                                renderTreeBuilder
                                   .OpenComponent<MudIconButton>()
                                   .AddAttribute(nameof(MudIconButton.Icon), context.Enabled ? Icons.Material.Outlined.LockOpen : Icons.Material.Outlined.Lock)
                                   .AddAttribute(nameof(MudIconButton.Color), context.Enabled ? Color.Primary : Color.Error)
                                   .AddAttribute(nameof(MudIconButton.OnClick), OnDisabledChanged.InvokeAsync(context))
                                   .Close();
                            }

                            if (TableConfiguration.EnableCustomAction)
                            {
                                foreach (var autoTableAction in TableConfiguration.CustomAutoTableActions)
                                {
                                    renderTreeBuilder
                                       .OpenComponent<MudIconButton>()
                                       .AddAttribute(nameof(MudIconButton.Icon), autoTableAction.Icon)
                                       .AddAttribute(nameof(MudIconButton.Color), autoTableAction.Color ?? Color.Primary)
                                       .AddAttribute(nameof(MudIconButton.OnClick), autoTableAction.OnValueChanged)
                                       .Close();
                                }
                            }
                        })
                       .Close();
                })
               .Close();
        }

        return treeBuilder => treeBuilder.AddContent(0, builder);
    }

    private RenderFragment BuildPagerContent(EasyRenderTreeBuilder builder)
    {
        builder
           .OpenComponent<MudTablePager>()
           .AddAttribute(nameof(MudTablePager.PageSizeOptions), _qtdPageSize)
           .AddAttribute(nameof(MudTablePager.RowsPerPageString), "Rows per page:")
           .AddAttribute(nameof(MudTablePager.InfoFormat), "{first_item}-{last_item} of {all_items}")
           .AddAttribute(nameof(MudTablePager.AllItemsText),"All")
           .AddAttribute(nameof(MudTablePager.HorizontalAlignment), HorizontalAlignment.Right)
           .Close();
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