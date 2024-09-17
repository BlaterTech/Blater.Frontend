namespace Blater.Frontend.Charts.Options;

public class ForecastDataPoints
{
    public int Count { get; set; } = 0;
    public double FillOpacity { get; set; } = 0.5;
    public int? StrokeWidth { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public int DashArray { get; set; } = 4;
}