namespace Blater.Frontend.Auto.AutoTable.Models;

public class ColumnConfiguration
{
    public string DataFormat { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string HasColumnName { get; set; } = string.Empty;
    public int MaxLength { get; set; }
    public int Order { get; set; }
    public int MergeColumn { get; set; }
    public bool IsRequired { get; set; }
}