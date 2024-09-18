using Blater.Enumerations;

namespace Blater.Frontend.Client.Interfaces;

public interface ITranslation
{
    public int Priority { get; set; }
    public LanguageTranslation Language { get; set; }
    public Dictionary<string, string> Dictionary { get; set; }
}