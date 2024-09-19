using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

public class AutoDetailsTabsPropertyConfiguration<TModel, TPropertyValue> : 
    BaseAutoPropertyConfiguration<TPropertyValue>, IAutoDetailsTabsPropertyConfiguration<TModel>
{
    public string? HeadTitle { get; set; }
}