using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Models;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Table;

public class AutoTableConfiguration<TModel>(string title)
{
    public string Title { get; } = title;

    public bool EnableDefaultAction { get; } = true;
    public bool EnableCreateButton { get; } = true;
    public bool EnableCustomAction { get; }
    public bool EnableFixedHeader { get; }
    public bool EnableFixedFooter { get; }
    public bool EnableMultiSelection { get; }
    public bool EnableSelectOnRowClick { get; }
    public bool EnableSelectionChangeable { get; }
    public bool EnableStriped { get; }

    public Color LoadingProgressColor { get; } = Color.Primary;
    
    
    public List<CustomAutoTableAction<TModel>> CustomAutoTableActions { get; } = [];
    public List<IAutoTablePropertyConfiguration<TModel>> Configurations { get; } = [];
}