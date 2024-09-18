using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Table;

public class AutoTablePropertyConfiguration<TModel, TPropertyValue> : BaseAutoPropertyConfiguration<TPropertyValue>, IAutoTablePropertyConfiguration<TModel>
{
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
}