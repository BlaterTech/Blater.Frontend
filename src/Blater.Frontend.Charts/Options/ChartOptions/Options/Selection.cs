namespace Blater.Frontend.Charts.Options.ChartOptions.Options;

public class Selection
{
    public bool Enabled { get; set; } = true;
    public string Type { get; set; } = "x";
    public Fill Fill { get; set; } = new();
    public Stroke Stroke { get; set; } = new();
    public Axis XAxis { get; set; } = new();
    public Axis YAxis { get; set; } = new();
}

public class Fill
{
    public string Color { get; set; } = "#24292e";
    public double Opacity { get; set; } = 0.1;
}

public class Stroke
{
    public int Width { get; set; } = 1;
    public int DashArray { get; set; } = 3;
    public string Color { get; set; } = "#24292e";
    public double Opacity { get; set; } = 0.4;
}

public class Axis
{
    public double? Min { get; set; } = null;
    public double? Max { get; set; } = null;
}