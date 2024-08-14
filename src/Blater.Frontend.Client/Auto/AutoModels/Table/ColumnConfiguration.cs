namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class ColumnConfiguration : BasePropertyConfiguration
{
    public string Name { get; set; } = string.Empty;
    
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
}