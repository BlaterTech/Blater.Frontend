namespace Blater.Frontend.Charts.Options.ChartOptions.Options;

public class DropShadow
{
    public bool Enabled { get; set; } = false;
    public List<int>? EnabledOnSeries { get; set; } = null;
    public int Top { get; set; } = 0;
    public int Left { get; set; } = 0;
    public int Blur { get; set; } = 3;
    public string Color { get; set; } = "#000";
    public double Opacity { get; set; } = 0.35;
}