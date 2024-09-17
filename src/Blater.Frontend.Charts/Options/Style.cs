namespace Blater.Frontend.Charts.Options;

public class Style
{
    public string FontSize { get; set; } = "14px";
    public string FontFamily { get; set; } = "Helvetica, Arial, sans-serif";
    public string FontWeight { get; set; } = "bold";
    public string? Colors { get; set; } = null;
    public string? Color { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
}