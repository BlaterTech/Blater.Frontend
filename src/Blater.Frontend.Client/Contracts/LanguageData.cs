using Blater.Enumerations;

namespace Blater.Frontend.Client.Contracts;

public class LanguageData
{
    public LanguageTranslation Language { get; set; }
    public string? Code { get; set; }
    public string? DateFormat { get; set; }
    public string? CurrencySymbol { get; set; }
    public string? CurrencySeparator { get; set; }
}