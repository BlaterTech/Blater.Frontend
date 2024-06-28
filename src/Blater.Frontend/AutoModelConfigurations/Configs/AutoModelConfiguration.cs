// ReSharper disable CollectionNeverQueried.Global

namespace Blater.Frontend.AutoModelConfigurations.Configs;

public class AutoModelConfiguration
{
    public required Type ModelType { get; set; }
    public required string ModelName { get; set; }
    public List<AutoComponentConfiguration> ComponentConfigurations { get; set; } = new();
    
    public AutoGridConfiguration Grid { get; internal set; } = null!;
    
    /// <summary>
    ///     Shows the toggle on before the card
    /// </summary>
    public bool CanBeDisabled { get; set; } = true;
}