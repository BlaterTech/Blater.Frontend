using Blater.Enumerations;

namespace Blater.Frontend.Models;


public class LanguageData
{
    public LanguageTranslation Language { get; set; }

    public string Code { get; set; } = null!;
    public string DateFormat { get; set; } = null!;
    public string CurrencySymbol { get; set; } = null!;
    public string CurrencySeparator { get; set; } = null!;
}