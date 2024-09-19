using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsGroupConfiguration<TModel>(string title) : BaseAutoGroupConfiguration(title)
{
    public bool EnableEditButton { get; set; } = true;
    public List<IAutoDetailsPropertyConfiguration<TModel>> Components { get; set; } = [];
}