using Blater.Frontend.Enumerations;
using Blater.Frontend.Helpers;
using Blater.Frontend.Interfaces;
using Blater.Frontend.Logging;
using Blater.Frontend.Models;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Services;

public class LocalizationService
{
    private static Dictionary<string, string> _dictionary = new();
    private readonly ILogger<LocalizationService> _logger;
    private readonly IEnumerable<ITranslation> _translations;

    public LocalizationService(ILogger<LocalizationService> logger, IEnumerable<ITranslation> translations)
    {
        _logger = logger;
        _translations = translations;

        foreach (var translation in _translations)
        {
            _logger.LogInformation("Loaded {Language} translation", translation.GetType());
        }

        // Default language
        ChangeLanguage(LanguageTranslation.Portugues);

        HotReloadHelper.UpdateApplicationEvent += HotReloadHelperOnUpdateApplicationEvent;
    }

    public LanguageData? SelectedLanguageData { get; private set; }
    public string DateFormat => SelectedLanguageData?.DateFormat ?? "dd/MM/yyyy";

    public static event Action? LocalizationChanged;


    private void HotReloadHelperOnUpdateApplicationEvent(Type[]? obj)
    {
        using var _ = new LogTimer("Reloading translations");
        ChangeLanguage(SelectedLanguageData?.Language ?? LanguageTranslation.Portugues);
    }

    public void ChangeLanguage(LanguageTranslation language)
    {
        _dictionary.Clear();

        var translations = _translations
                          .Where(x => x.Language == language)
                          .OrderByDescending(x => x.Priority)
                          .ToList();

        if (translations.Count == 0)
        {
            _logger.LogError("No translations found for {Language}", language);
            return;
        }

        var dictionaries = translations
           .SelectMany(x => x.Dictionary);
        
        foreach (var (key, value) in dictionaries)
        {
            if (_dictionary.TryAdd(key, value))
            {
                continue;
            }

            _logger.LogWarning("Key {Key} already exists in {Language}", key, language);
        }

        SelectedLanguageData = new LanguageData
        {
            Language = language
        };

        LocalizationChanged?.Invoke();
    }

    public string Get(string id)
    {
        if (id.EndsWith("Page"))
        {
            id = id.Replace("Page", "");
        }

        try
        {
            if (_dictionary.TryGetValue(id, out var value))
            {
                return value;
            }

            _logger.LogWarning("|{id}| does not exists in {Language}", id, SelectedLanguageData?.Language);
            return $"|{id}| does not exists in {SelectedLanguageData?.Language}";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting translation for {Id}", id);
            return $"|{id}| does not exists in {SelectedLanguageData?.Language}";
        }
    }

    public string Get(object obj)
    {
        var typeName = obj.GetType().Name;
        return Get(typeName);
    }

    public string? GetOrDefault(object obj)
    {
        var typeName = obj.GetType().Name;
        return GetOrDefault(typeName);
    }

    public string? GetOrDefault(string id)
    {
        return _dictionary.TryGetValue(id, out var value) ? value : default;
    }
}