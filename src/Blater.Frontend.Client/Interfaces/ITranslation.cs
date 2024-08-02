using Blater.Enumerations;

namespace Blater.Frontend.Client.Interfaces;

public interface ITranslation
{
    public int Priority { get; }
    public LanguageTranslation Language { get; }
    public Dictionary<string, string> Dictionary { get; }
}