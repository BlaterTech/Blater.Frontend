namespace Blater.Frontend.Charts.Options;

public class Grid
{
    public bool Show { get; set; } = true;
    public string BorderColor { get; set; } = "#90A4AE";
    public int StrokeDashArray { get; set; } = 0;
    public string Position { get; set; } = "back";
    public Axis Xaxis { get; set; } = new();
    public Axis Yaxis { get; set; } = new();
    public GridColors Row { get; set; } = new();
    public GridColors Column { get; set; } = new();
    public Padding Padding { get; set; } = new();
}

public class Lines
{
    public bool Show { get; set; } = false;
}

public class Axis
{
    public Lines Lines { get; set; } = new();
}

public class Padding
{
    public int Top { get; set; } = 0;
    public int Right { get; set; } = 0;
    public int Bottom { get; set; } = 0;
    public int Left { get; set; } = 0;
}

public class GridColors
{
    public string? Colors { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public double Opacity { get; set; } = 0.5;
}