namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsGroupConfiguration
{
    public AutoDetailsGroupConfiguration(string title)
    {
        Title = title;
    }
    public string Title { get; set; }
    public bool EnableEditButton { get; set; } = true;
    public List<AutoDetailsAutoPropertyConfiguration> Components { get; set; } = [];
}