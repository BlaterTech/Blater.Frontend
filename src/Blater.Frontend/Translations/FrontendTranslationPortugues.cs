using Blater.Enumerations;
using Blater.Frontend.Interfaces;

namespace Blater.Frontend.Translations;

public class FrontendTranslationPortugues : ITranslation
{
    public int Priority => -100;
    public LanguageTranslation Language => LanguageTranslation.Portugues;
    public Dictionary<string, string> Dictionary => [];
}