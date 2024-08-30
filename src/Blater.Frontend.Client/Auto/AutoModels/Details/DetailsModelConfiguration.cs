namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class DetailsModelConfiguration
{
    public string Title { get; set; } = string.Empty;
    
    public string SubTitle { get; set; } = string.Empty;
    
    public bool DisableEditButton { get; set; }

    public List<DetailsGroupConfiguration> Configurations { get; set; } = [];
}