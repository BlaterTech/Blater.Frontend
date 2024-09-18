using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsConfiguration<TModel>(string title)
{
    public string Title { get; set; } = title;
    public string? ExtraClass { get; set; }
    public bool EnableBackButton { get; set; } = true;

    public AutoAvatarModelConfiguration? AvatarConfiguration { get; set; }
    public List<AutoDetailsGroupConfiguration<TModel>> Groups { get; set; } = [];
}