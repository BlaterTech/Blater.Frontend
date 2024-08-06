using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Components.AutoTable.Models;

public class TableConfiguration<TTable> where TTable : BaseDataModel
{
    public string ToTable { get; set; } = $"Blater-AutoTable-{nameof(TTable)}";
    public bool EnabledFixedHeader { get; set; }
    public bool EnabledFixedFooter { get; set; }
    public IList<ColumnConfiguration> Columns { get; } = [];
}