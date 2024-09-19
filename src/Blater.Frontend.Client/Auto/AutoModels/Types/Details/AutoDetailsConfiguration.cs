using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsConfiguration<TModel>(string title) : BaseAutoConfiguration(title)
{
    public string? ExtraClass { get; set; }
    public bool EnableBackButton { get; set; } = true;

    public AutoAvatarModelConfiguration? AvatarConfiguration { get; set; }
    public List<AutoDetailsGroupConfiguration<TModel>> Groups { get; set; } = [];
}