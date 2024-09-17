using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class BaseAutoDetailsBaseAutoPropertyConfiguration<TPropertyValue> : BaseAutoPropertyConfiguration<TPropertyValue>
{
    public string? Title { get; set; }
    public bool SeparateCard { get; set; }
    public bool SeparateComponent { get; set; }
}