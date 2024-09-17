using System.Globalization;

namespace Blater.Frontend.Charts.Options.ChartOptions.Options;

public class Toolbar
{
    public bool Show { get; set; } = true;
    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;
    public Tools Tools { get; set; } = new();
    public Export Export { get; set; } = new();
    public string AutoSelected { get; set; } = "zoom";
}

public class Tools
{
    public bool Download { get; set; } = true;
    public bool Selection { get; set; } = true;
    public bool Zoom { get; set; } = true;
    public bool ZoomIn { get; set; } = true;
    public bool ZoomOut { get; set; } = true;
    public bool Pan { get; set; } = true;
    public object Reset { get; set; } = true;
    public List<string> CustomIcons { get; set; } = [];
}

public class CsvExport
{
    public string? Filename { get; set; } = null;
    public string ColumnDelimiter { get; set; } = ",";
    public string HeaderCategory { get; set; } = "category";
    public string HeaderValue { get; set; } = "value";

    public string CategoryFormatter(double x)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds((long)x).DateTime.ToString("ddd MMM dd yyyy");
    }

    public string ValueFormatter(double y)
    {
        return y.ToString(CultureInfo.InvariantCulture);
    }
}

public class SvgExport
{
    public string? Filename { get; set; } = null;
}

public class PngExport
{
    public string? Filename { get; set; } = null;
}

public class Export
{
    public CsvExport Csv { get; set; } = new();
    public SvgExport Svg { get; set; } = new();
    public PngExport Png { get; set; } = new();
}