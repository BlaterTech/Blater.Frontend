namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsGroupConfiguration
{
    public required string Title { get; set; } = string.Empty;
    public bool EnableEditButton { get; set; } = true;
    public List<AutoDetailsAutoComponentConfiguration> Components { get; set; } = [];
}