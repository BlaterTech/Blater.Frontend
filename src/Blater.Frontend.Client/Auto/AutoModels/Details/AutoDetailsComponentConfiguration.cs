using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class AutoDetailsAutoComponentConfiguration : BaseAutoComponentConfiguration
{
    public string? Title { get; set; }
    public bool SeparateCard { get; set; }
    public bool SeparateComponent { get; set; }
}