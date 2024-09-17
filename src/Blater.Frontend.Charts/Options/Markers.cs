namespace Blater.Frontend.Charts.Options;


public class Markers
{
    public int Size { get; set; } = 0;
    public string? Colors { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public string StrokeColors { get; set; } = "#fff";
    public int StrokeWidth { get; set; } = 2;
    public double StrokeOpacity { get; set; } = 0.9;
    public int StrokeDashArray { get; set; } = 0;
    public double FillOpacity { get; set; } = 1;
    public List<object> Discrete { get; set; } = []; // Lista vazia para valores discretos
    public string Shape { get; set; } = "circle";
    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;
    public Action? OnClick { get; set; } = null;
    public Action? OnDblClick { get; set; } = null;
    public bool ShowNullDataPoints { get; set; } = true;
    public Hover Hover { get; set; } = new();
}

public class Hover
{
    public int? Size { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public int SizeOffset { get; set; } = 3;
}