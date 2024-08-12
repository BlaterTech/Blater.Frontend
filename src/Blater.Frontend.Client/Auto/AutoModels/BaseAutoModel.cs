namespace Blater.Frontend.Client.Auto.AutoModels;

public abstract class BaseAutoModel<T>
{
    public string Title { get; set; } = null!;
    public string? CssClass { get; set; }
    public string? CssStyle { get; set; }
}