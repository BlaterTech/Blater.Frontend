using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsAutoComponentConfiguration : BaseAutoComponentConfiguration
{
    public string? Title { get; set; }
    public bool SeparateCard { get; set; }
    public bool SeparateComponent { get; set; }
}