using Blater.Enumerations;
using Blater.Frontend.Client.Contracts;
using Blater.Frontend.Client.Helpers;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Logging;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Client.Services;

public class LocalizationService : ILocalizationService, IDisposable
{
    private static readonly Dictionary<string, string> Dictionary = new();
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
        ChangeLanguage(LanguageTranslation.Portuguese);

        HotReloadHelper.UpdateApplicationEvent += HotReloadHelperOnUpdateApplicationEvent;
    }

    public LanguageData? SelectedLanguageData { get; private set; }
    public string DateFormat => SelectedLanguageData?.DateFormat ?? "dd/MM/yyyy";

    public event Action? LocalizationChanged;


    private void HotReloadHelperOnUpdateApplicationEvent(Type[]? obj)
    {
        using var _ = new LogTimer("Reloading translations");
        ChangeLanguage(SelectedLanguageData?.Language ?? LanguageTranslation.Portuguese);

        //Debug
        /*foreach (var translation in _dictionary)
        {
            Console.WriteLine($"{translation.Key} - {translation.Value}");
        }*/
    }

    public void ChangeLanguage(LanguageTranslation language)
    {
        Dictionary.Clear();

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
            if (Dictionary.TryAdd(key, value))
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

    public string GetValue(string id)
    {
        if (id.EndsWith("Page"))
        {
            id = id.Replace("Page", "");
        }

        try
        {
            if (Dictionary.TryGetValue(id, out var value))
            {
                return value;
            }

            _logger.LogWarning("|{Id}| does not exists in {Language}", id, SelectedLanguageData?.Language);
            return $"|{id}| does not exists in {SelectedLanguageData?.Language}";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting translation for {Id}", id);
            return $"|{id}| does not exists in {SelectedLanguageData?.Language}";
        }
    }

    public string GetValue(object obj)
    {
        var typeName = obj.GetType().Name;
        return GetValue(typeName);
    }

    public string? GetValueOrDefault(object obj)
    {
        var typeName = obj.GetType().Name;
        return GetValueOrDefault(typeName);
    }

    public string? GetValueOrDefault(string id)
    {
        return Dictionary.GetValueOrDefault(id);
    }

    public bool TryAddValue(string key, object value)
    {
        var stringValue = value.ToString();
        
        return Dictionary.TryAdd(key, stringValue ?? string.Empty);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}