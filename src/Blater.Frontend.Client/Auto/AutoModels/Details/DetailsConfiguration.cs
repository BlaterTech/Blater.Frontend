namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class DetailsConfiguration
{
    public string Name { get; set; } = string.Empty;
    
    public string SubTitle { get; set; } = string.Empty;
    
    public bool DisableEditButton { get; set; }
    
    public IList<DetailsGroupConfiguration>? GroupsConfigurations { get; set; }

    public IList<DetailsPropertyConfiguration> PropertyConfigurations { get; set; } = [];
}