using Blater.Frontend.Charts.Options.Enumerations;

namespace Blater.Frontend.Charts.Options.ChartOptions.Options;

public class Zoom
{
    public bool Enabled { get; set; } = true;
    public ZoomType Type { get; set; }
    public bool AutoScaleYaxis { get; set; } = false;
    public ZoomedArea ZoomedArea { get; set; } = new();
}

public class ZoomedAreaFill
{
    public string Color { get; set; } = "#90CAF9";
    public double Opacity { get; set; } = 0.4;
}

public class ZoomedAreaStroke
{
    public string Color { get; set; } = "#0D47A1";
    public double Opacity { get; set; } = 0.4;
    public int Width { get; set; } = 1;
}

public class ZoomedArea
{
    public ZoomedAreaFill Fill { get; set; } = new();
    public ZoomedAreaStroke Stroke { get; set; } = new();
}