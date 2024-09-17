namespace Blater.Frontend.Charts.Options;

public class NoData
{
    public string? Text { get; set; } = null;
    public string Align { get; set; } = "center";
    public string VerticalAlign { get; set; } = "middle";
    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;
    public Style Style { get; set; } = new Style();   
}