using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Table;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoModelConfiguration
{
    public required Type ModelType { get; set; }
    public required string ModelName { get; set; }

    public FormModelConfiguration FormModelConfiguration { get; set; } = new();
    public DetailsModelConfiguration DetailsModelConfiguration { get; set; } = new();
    public TableModelConfiguration TableModelConfiguration { get; set; } = new();
    
    /// <summary>
    ///     Shows the toggle on before the card
    /// </summary>
    public bool CanBeDisabled { get; set; } = true;
}