using System.Reflection;

namespace Blater.Frontend.Client.Auto.Components.AutoTable.Models;

public class ColumnConfiguration
{
    public PropertyInfo PropertyInfo { get; set; } = null!;
    public string DataFormat { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string HasColumnName { get; set; } = string.Empty;
    public int MaxLength { get; set; }
    public int Order { get; set; }
    public int MergeColumn { get; set; }
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
}