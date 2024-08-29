namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class TableConfiguration
{
    public string Name { get; set; } = string.Empty;
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public IList<TablePropertyConfiguration> Properties { get; } = [];
}