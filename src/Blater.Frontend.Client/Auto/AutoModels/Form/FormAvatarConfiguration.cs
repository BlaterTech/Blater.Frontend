namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class AutoAvatarModelConfiguration
{
    public bool EnableAvatarModel { get; set; }
    public bool ContainerPublic { get; set; } = true;
    public string AvatarUrl { get; set; } = "default-image-value-url";
    public string ContainerPrefix { get; set; } = "avatar";
    public string ExtraClass { get; set; } = string.Empty;
    public string ExtraStyle { get; set; } = string.Empty;
}