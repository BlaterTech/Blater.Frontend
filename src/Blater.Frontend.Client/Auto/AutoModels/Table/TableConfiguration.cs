using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class TableConfiguration : BaseConfiguration
{
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public IList<TablePropertyConfiguration> Columns { get; } = [];
}