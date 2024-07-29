using Blater.Enumerations;
using Blater.FrontEnd.Interfaces;

namespace Blater.FrontEnd.Translations;

public class FrontendTranslationPortugues : ITranslation
{
    public int Priority => -100;
    public LanguageTranslation Language => LanguageTranslation.Portugues;
    public Dictionary<string, string> Dictionary => [];
}