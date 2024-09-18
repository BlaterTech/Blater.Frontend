using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsConfiguration<TModel>(string title)
{
    public string Title { get; } = title;
    public string? ExtraClass { get; }
    public bool EnableBackButton { get; } = true;

    public AutoAvatarModelConfiguration<TModel>? AvatarConfiguration { get; }
    public List<AutoDetailsGroupConfiguration<TModel>> Groups { get; } = [];
}