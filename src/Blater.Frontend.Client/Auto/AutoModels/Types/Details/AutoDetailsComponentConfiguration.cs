using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsAutoPropertyConfiguration : BaseAutoPropertyConfiguration<>
{
    public string? Title { get; set; }
    public bool SeparateCard { get; set; }
    public bool SeparateComponent { get; set; }
}