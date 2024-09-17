using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsGroupConfiguration
{
    public AutoDetailsGroupConfiguration(string title)
    {
        Title = title;
    }
    public string Title { get; set; }
    public bool EnableEditButton { get; set; } = true;
    public List<IAutoDetailsPropertyConfiguration> Components { get; set; } = [];
}