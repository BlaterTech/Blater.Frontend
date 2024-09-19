using Blater.Frontend.Client.Auto.AutoModels.Base;
using Microsoft.IdentityModel.Tokens;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoAvatarModelConfiguration(string title) : BaseAutoAvatarConfiguration(title)
{
    public bool EnableAvatarModel { get; set; }
    public bool ContainerPublic { get; set; } = true;
    public string AvatarUrl { get; set; } = "default-image-value-url";
    public string ContainerPrefix { get; set; } = "avatar";
    public string ExtraClass { get; set; } = string.Empty;
    public string ExtraStyle { get; set; } = string.Empty;
}

public abstract class BaseAutoAvatarConfiguration(string title) : BaseAutoConfiguration(title)
{
    public string SubTitle { get; set; } = string.Empty;
    public string SubTitleLocalizationId { get; set; } = "";
}