namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseAutoConfiguration(string title)
{
    public string LocalizationId { get; set; } = "";
    public string Title { get; set; } = title;
}