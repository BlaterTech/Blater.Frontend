namespace Blater.Frontend.Charts.Options.ChartOptions.Options;

public class AnimateGradually
{
    public bool Enabled { get; set; } = true;
    public int Delay { get; set; } = 150;
}

public class DynamicAnimation
{
    public bool Enabled { get; set; } = true;
    public int Speed { get; set; } = 350;
}

public class Animations
{
    public bool Enabled { get; set; } = true;
    public string Easing { get; set; } = "easeinout";
    public int Speed { get; set; } = 800;
    public AnimateGradually AnimateGradually { get; set; } = new();
    public DynamicAnimation DynamicAnimation { get; set; } = new();
}