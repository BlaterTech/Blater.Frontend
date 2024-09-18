namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoAvatarModelConfiguration<TModel>
{
    public bool EnableAvatarModel { get; }
    public bool ContainerPublic { get; set; } = true;
    public string AvatarUrl { get; } = "default-image-value-url";
    public string ContainerPrefix { get; set; } = "avatar";
    public string ExtraClass { get; } = string.Empty;
    public string ExtraStyle { get; } = string.Empty;
    public string Title { get; } = string.Empty;
    public string SubTitle { get; } = string.Empty;
}