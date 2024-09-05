namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class AutoTableConfiguration
{
    public required string Title { get; set; }
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public List<AutoTableAutoComponentConfiguration> Configurations { get; set; } = [];
}