namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class ColumnConfiguration<T> : BasePropertyConfiguration<T>
{
    public string Name { get; set; } = string.Empty;
    
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
}