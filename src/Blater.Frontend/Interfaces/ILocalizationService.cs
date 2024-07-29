using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations;
using Blater.FrontEnd.Models;
#pragma warning disable CS0067 // Event is never used

namespace Blater.FrontEnd.Interfaces;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface ILocalizationService
{
    LanguageData? SelectedLanguageData { get; }
    string DateFormat { get; }
    static event Action? LocalizationChanged;
    void ChangeLanguage(LanguageTranslation language);
    string Get(string id);
    string Get(object obj);
    string? GetOrDefault(object obj);
    string? GetOrDefault(string id);
    void Dispose();
}