using Blater.Frontend.Client.Models;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class AutoTableConfiguration
{
    public required string Title { get; set; }

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
    
    
    public List<CustomAutoTableAction> CustomAutoTableActions { get; set; } = [];
    public List<AutoTableAutoComponentConfiguration> Configurations { get; set; } = [];
}