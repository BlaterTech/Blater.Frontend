using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Contracts;

using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Table;

public class AutoTableConfiguration<TModel>(string title) : BaseAutoConfiguration(title)
{
    public bool EnableDefaultAction { get; set; } = true;
    public bool EnableShowLockButton { get; set; } = true;
    public bool EnableCreateButton { get; set; } = true;
    public AutoFormType AutoFormType { get; set; } = AutoFormType.Form;
    public AutoDetailsType AutoDetailsType { get; set; } = AutoDetailsType.Details;
    public bool EnableCustomAction { get; set; }
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public bool EnableMultiSelection { get; set; }
    public bool EnableSelectOnRowClick { get; set; }
    public bool EnableSelectionChangeable { get; set; }
    public bool EnableStriped { get; set; }

    public Color LoadingProgressColor { get; set; } = Color.Primary;

    public AutoTablePagerConfiguration PagerConfiguration { get; set; } = new();
    public List<CustomAutoTableAction> CustomAutoTableActions { get; set; } = [];
    public List<IAutoTablePropertyConfiguration<TModel>> CustomAutoTableFilters { get; set; } = [];
    public List<IAutoTablePropertyConfiguration<TModel>> Configurations { get; set; } = [];
}