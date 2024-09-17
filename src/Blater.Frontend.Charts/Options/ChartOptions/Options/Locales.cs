namespace Blater.Frontend.Charts.Options.ChartOptions.Options;

public class Locales
{
    public List<Locale> LocaleList { get; set; } = [];
}

public class LocaleToolbar
{
    public string Download { get; set; } = "Download SVG";
    public string Selection { get; set; } = "Selection";
    public string SelectionZoom { get; set; } = "Selection Zoom";
    public string ZoomIn { get; set; } = "Zoom In";
    public string ZoomOut { get; set; } = "Zoom Out";
    public string Pan { get; set; } = "Panning";
    public string Reset { get; set; } = "Reset Zoom";
}

public class LocaleOptions
{
    public List<string> Months { get; set; } = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    
    public List<string> ShortMonths { get; set; } = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    
    public List<string> Days { get; set; } = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    
    public List<string> ShortDays { get; set; } = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
    
    public LocaleToolbar Toolbar { get; set; } = new();
}

public class Locale
{
    public string Name { get; set; } = "en";
    public LocaleOptions Options { get; set; } = new();
}