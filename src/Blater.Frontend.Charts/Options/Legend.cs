namespace Blater.Frontend.Charts.Options;

public class Legend
{
    public bool Show { get; set; } = true;
    public bool ShowForSingleSeries { get; set; } = false;
    public bool ShowForNullSeries { get; set; } = true;
    public bool ShowForZeroSeries { get; set; } = true;
    public string Position { get; set; } = "bottom";
    public string HorizontalAlign { get; set; } = "center";
    public bool Floating { get; set; } = false;
    public string FontSize { get; set; } = "14px";
    public string FontFamily { get; set; } = "Helvetica, Arial";
    public int FontWeight { get; set; } = 400;
    public Func<string, string>? Formatter { get; set; } = null; // Função para formatação
    public bool InverseOrder { get; set; } = false;
    public int? Width { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public int? Height { get; set; } = null;
    public Func<string, string>? TooltipHoverFormatter { get; set; } = null; // Função para formatação do tooltip
    public List<object> CustomLegendItems { get; set; } = []; // Lista vazia para itens personalizados
    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;
    public LegendLabels Labels { get; set; } = new();
    public Markers Markers { get; set; } = new();
    public ItemMargin ItemMargin { get; set; } = new();
    public OnItemClick OnItemClick { get; set; } = new();
    public OnItemHover OnItemHover { get; set; } = new();
}

public class LegendLabels
{
    public string? Colors { get; set; } = null; // `undefined` em JS pode ser representado por `null` em C#
    public bool UseSeriesColors { get; set; } = false;
}

public class ItemMargin
{
    public int Horizontal { get; set; } = 5;
    public int Vertical { get; set; } = 0;
}

public class OnItemClick
{
    public bool ToggleDataSeries { get; set; } = true;
}

public class OnItemHover
{
    public bool HighlightDataSeries { get; set; } = true;
}