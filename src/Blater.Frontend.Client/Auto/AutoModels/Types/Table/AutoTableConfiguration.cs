using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Models;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Table;

public class AutoTableConfiguration<TModel>(string title) : BaseAutoConfiguration
{
    public string Title { get; set; } = title;

    public bool EnableDefaultAction { get; set; } = true;
    public bool EnableCreateButton { get; set; } = true;
    public bool EnableCustomAction { get; set; }
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public bool EnableMultiSelection { get; set; }
    public bool EnableSelectOnRowClick { get; set; }
    public bool EnableSelectionChangeable { get; set; }
    public bool EnableStriped { get; set; }

    public Color LoadingProgressColor { get; set; } = Color.Primary;

    public List<CustomAutoTableAction<TModel>> CustomAutoTableActions { get; set; } = [];
    public List<IAutoTablePropertyConfiguration<TModel>> Configurations { get; set; } = [];
}