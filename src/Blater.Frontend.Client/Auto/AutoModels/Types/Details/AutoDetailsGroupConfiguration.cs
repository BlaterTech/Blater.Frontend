using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsGroupConfiguration<TModel>(string title)
{
    public string Title { get; } = title;
    public bool EnableEditButton { get; } = true;
    public List<IAutoDetailsPropertyConfiguration<TModel>> Components { get; } = [];
}