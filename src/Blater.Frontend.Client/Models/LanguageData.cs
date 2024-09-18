using Blater.Enumerations;

namespace Blater.Frontend.Client.Models;

public class LanguageData
{
    public LanguageTranslation Language { get; set; }
    public string? Code { get; set; }
    public string? DateFormat { get; }
    public string? CurrencySymbol { get; set; }
    public string? CurrencySeparator { get; set; }
}