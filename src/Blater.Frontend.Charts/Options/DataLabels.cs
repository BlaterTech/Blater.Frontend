namespace Blater.Frontend.Charts.Options;

public class DataLabels
{
    public bool Enabled { get; set; } = true;
    public List<int>? EnabledOnSeries { get; set; } = null;
    public Func<double, object, object> Formatter { get; set; } = (val, opts) => val;
    public string TextAnchor { get; set; } = "middle";
    public bool Distributed { get; set; } = false;
    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;
    public Style Style { get; set; } = new();
    public Background Background { get; set; } = new();
    public DropShadow DropShadow { get; set; } = new();
}

public class DropShadow
{
    public bool Enabled { get; set; } = false;
    public int Top { get; set; } = 1;
    public int Left { get; set; } = 1;
    public int Blur { get; set; } = 1;
    public string Color { get; set; } = "#000";
    public double Opacity { get; set; } = 0.45;
}

public class Background
{
    public bool Enabled { get; set; } = true;
    public string ForeColor { get; set; } = "#fff";
    public int Padding { get; set; } = 4;
    public int BorderRadius { get; set; } = 2;
    public int BorderWidth { get; set; } = 1;
    public string BorderColor { get; set; } = "#fff";
    public double Opacity { get; set; } = 0.9;
    public DropShadow DropShadow { get; set; } = new();
}