using Blater.Enumerations;

#pragma warning disable CS0067 // Event is never used

namespace Blater.Frontend.Client.Interfaces;

public interface ILocalizationService
{
    event Action? LocalizationChanged;
    void ChangeLanguage(LanguageTranslation language);
    string GetValue(string id);
    string GetValue(object obj);
    string? GetValueOrDefault(object obj);
    string? GetValueOrDefault(string id);
    bool TryAddValue(string key, object value);
    void Dispose();
}