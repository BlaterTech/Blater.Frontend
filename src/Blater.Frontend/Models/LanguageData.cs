using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations;

namespace Blater.FrontEnd.Models;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class LanguageData
{
    public LanguageTranslation Language { get; set; }

    public string Code { get; set; } = null!;
    public string DateFormat { get; set; } = null!;
    public string CurrencySymbol { get; set; } = null!;
    public string CurrencySeparator { get; set; } = null!;
}