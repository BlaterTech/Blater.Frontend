using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoModelConfiguration
{
    public required Type ModelType { get; set; }
    public required string ModelName { get; set; }

    public List<AutoGroupConfiguration> AutoGroupConfigurations { get; set; } = [];

    /// <summary>
    ///     Shows the toggle on before the card
    /// </summary>
    public bool CanBeDisabled { get; set; } = true;
}