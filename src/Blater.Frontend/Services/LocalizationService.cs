using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations;
using Blater.FrontEnd.Interfaces;
using Blater.FrontEnd.Models;
using Blater.Helpers;
using Blater.Logging;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Services;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
[SuppressMessage("Naming", "CA1727:Usar o PascalCase nos espaços reservados nomeados")]
[SuppressMessage("Globalization", "CA1310:Especificar StringComparison para garantir a exatidão")]
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

    public string Get(string id)
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
        return Dictionary.GetValueOrDefault(id);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}