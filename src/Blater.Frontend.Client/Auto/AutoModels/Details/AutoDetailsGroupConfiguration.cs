namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class AutoDetailsGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    public bool DisableEditButton { get; set; }
    public List<AutoDetailsAutoComponentConfiguration> Configurations { get; set; } = [];
}