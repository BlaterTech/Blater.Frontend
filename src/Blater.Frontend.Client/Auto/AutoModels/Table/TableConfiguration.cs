using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class TableConfiguration<T> where T : BaseDataModel
{
    public string Name { get; set; } = $"Blater-AutoTable-{nameof(T)}";
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public IList<ColumnConfiguration> Columns { get; } = [];
}