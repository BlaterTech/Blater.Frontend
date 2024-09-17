using Blater.Frontend.Charts.Options.ChartOptions.Options;
using Blater.Frontend.Charts.Options.Enumerations;

namespace Blater.Frontend.Charts.Options.ChartOptions;

public class Chart
{
    public Animations? Animations { get; set; }
    public Brush? Brush { get; set; }
    public DropShadow? DropShadow { get; set; }
    
    public string Background { get; set; } = "#FFF";
    public string DefaultLocale { get; set; } = "en";
    public Locales? Locales { get; set; }
    public string FontFamily { get; set; } = "";
    public string ForeColor { get; set; } = "";
    public string Group { get; set; } = "";
    public object? Height { get; set; }
    public string Id { get; set; } = "";
    public string? Nonce { get; set; }
    public int OffsetX { get; set; }
    public int OffsetY { get; set; }
    public int ParentHeightOffset { get; set; }
    public bool RedrawOnParentResize { get; set; }
    public bool RedrawOnWindowResize { get; set; }

    public Selection? Selection { get; set; }
    public Sparkline? Sparkline { get; set; }

    public bool Stacked { get; set; }
    public StackType? StackType { get; set; }
    public bool StackOnlyBar { get; set; }

    public Toolbar? Toolbar { get; set; }
    public ChartType ChartType { get; set; }

    public object? Width { get; set; }

    public Zoom? Zoom { get; set; }
}