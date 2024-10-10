using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsPropertyConfiguration<TModel, TPropertyValue>
    : BaseAutoPropertyConfiguration<TPropertyValue>, IAutoDetailsPropertyConfiguration<TModel>
{
    public bool SeparateCard { get; set; }
    public bool SeparateComponent { get; set; }
}