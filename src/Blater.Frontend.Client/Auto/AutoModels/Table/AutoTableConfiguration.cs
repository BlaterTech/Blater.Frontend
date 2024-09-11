using Blater.Frontend.Client.Models;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class AutoTableConfiguration
{
    public required string Title { get; set; }

    public bool EnableDefaultAction { get; set; } = true;
    public bool EnabledCreateButton { get; set; } = true;
    public bool EnableCustomAction { get; set; }
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public bool EnableMultiSelection { get; set; }
    public bool EnabledSelectOnRowClick { get; set; }
    public bool EnabledSelectionChangeable { get; set; }
    public bool EnabledStriped { get; set; }
    public bool Loading { get; set; }

    public Color LoadingProgressColor { get; set; } = Color.Primary;
    
    
    public List<CustomAutoTableAction> CustomAutoTableActions { get; set; } = [];
    public List<AutoTableAutoComponentConfiguration> Configurations { get; set; } = [];
}