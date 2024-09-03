namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class AutoDetailsModelConfiguration<T> 
{
    public string Title { get; set; } = string.Empty;
    
    public string SubTitle { get; set; } = string.Empty;
    
    public bool DisableEditButton { get; set; }

    public List<AutoDetailsGroupConfiguration> Configurations { get; set; } = [];
}