using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsPropertyConfiguration<TPropertyValue> : BaseAutoPropertyConfiguration<TPropertyValue>
{
    public string? HeadTitle { get; set; }
}