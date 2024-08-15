namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class DetailsConfiguration : BaseConfiguration
{
    public bool DisableEditButton { get; set; }
    
    public IList<DetailsGroupConfiguration>? GroupsConfigurations { get; set; }

    public IList<DetailsPropertyConfiguration> PropertyConfigurations { get; set; } = [];
}