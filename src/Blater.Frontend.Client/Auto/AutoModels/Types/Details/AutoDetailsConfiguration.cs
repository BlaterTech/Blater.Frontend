using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Details;

public class AutoDetailsConfiguration
{
    public required string Title { get; set; }
    public string? ExtraClass { get; set; }
    public bool EnableBackButton { get; set; } = true;

    public AutoAvatarModelConfiguration? AvatarConfiguration { get; set; }
    public List<AutoDetailsGroupConfiguration> Groups { get; set; } = [];
}