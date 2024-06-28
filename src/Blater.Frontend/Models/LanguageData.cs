using Blater.Frontend.Enumerations;

#pragma warning disable CS8618
namespace Blater.Frontend.Models;

public class LanguageData
{
    public LanguageTranslation Language { get; set; }
    public string Code { get; set; }
    public string DateFormat { get; set; }
    public string CurrencySymbol { get; set; }
    public string CurrencySeparator { get; set; }
}