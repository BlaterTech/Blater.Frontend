namespace Blater.Frontend.Charts.Options;

public class Fill
{
    public string? Colors { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public double Opacity { get; set; } = 0.9;
    public string Type { get; set; } = "solid";
    public Gradient Gradient { get; set; } = new();
    public Image Image { get; set; } = new();
    public Pattern Pattern { get; set; } = new();
}

public class Gradient
{
    public string Shade { get; set; } = "dark";
    public string Type { get; set; } = "horizontal";
    public double ShadeIntensity { get; set; } = 0.5;
    public List<string>? GradientToColors { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public bool InverseColors { get; set; } = true;
    public double OpacityFrom { get; set; } = 1;
    public double OpacityTo { get; set; } = 1;
    public List<int> Stops { get; set; } = [0, 50, 100];
    public List<object> ColorStops { get; set; } = []; // Pode ser uma lista vazia
}

public class Image
{
    public List<string> Src { get; set; } = []; // Lista vazia para src
    public int? Width { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public int? Height { get; set; } = null;
}

public class Pattern
{
    public string Style { get; set; } = "verticalLines";
    public int Width { get; set; } = 6;
    public int Height { get; set; } = 6;
    public int StrokeWidth { get; set; } = 2;
}