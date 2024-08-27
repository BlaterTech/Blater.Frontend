using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Table;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoModelConfiguration
{
    public required Type ModelType { get; set; }
    public required string ModelName { get; set; }
    
    public TableConfiguration Table { get; set; } = new();
    public FormConfiguration Form { get; set; } = new();
    public DetailsConfiguration Details { get; set; } = new();
    
    /// <summary>
    ///     Shows the toggle on before the card
    /// </summary>
    public bool CanBeDisabled { get; set; } = true;
}