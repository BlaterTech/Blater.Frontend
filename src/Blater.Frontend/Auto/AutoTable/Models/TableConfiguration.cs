using Blater.Frontend.Auto.AutoTable.Implementations;
using Blater.Frontend.Auto.AutoTable.Interfaces;
using Blater.Models.Bases;

namespace Blater.Frontend.Auto.AutoTable.Models;

public class TableConfiguration<TTable> where TTable : BaseDataModel
{
    public string ToTable { get; set; } = $"Blater-AutoTable-{nameof(TTable)}";
    public IList<ColumnConfiguration> Columns { get; } = [];
}