namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class TableModelConfiguration
{
    public string Title { get; set; } = string.Empty;
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public List<TableComponentConfiguration> Properties { get; } = [];
}