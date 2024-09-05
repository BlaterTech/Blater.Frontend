namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class AutoDetailsConfiguration
{
    public required string Title { get; set; }
    
    public string SubTitle { get; set; } = string.Empty;
    
    public bool DisableEditButton { get; set; }

    public List<AutoDetailsGroupConfiguration> Configurations { get; set; } = [];
}