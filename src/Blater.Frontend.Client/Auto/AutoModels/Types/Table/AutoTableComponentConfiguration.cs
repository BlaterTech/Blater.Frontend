using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Table;

public class AutoTableAutoPropertyConfiguration<TPropertyValue> : BaseAutoPropertyConfiguration<TPropertyValue>
{
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
}