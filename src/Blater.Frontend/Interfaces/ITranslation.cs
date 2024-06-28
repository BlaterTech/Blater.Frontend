using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations;

namespace Blater.FrontEnd.Interfaces;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public interface ITranslation
{
    public int Priority { get; }
    public LanguageTranslation Language { get; }
    public Dictionary<string, string> Dictionary { get; }
}