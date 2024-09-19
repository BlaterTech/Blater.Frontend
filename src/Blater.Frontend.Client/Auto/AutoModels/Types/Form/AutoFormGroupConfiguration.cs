using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormGroupConfiguration<TModel>(string title) : BaseAutoGroupConfiguration(title)
{
    public List<AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public List<IAutoFormPropertyConfiguration<TModel>> ComponentConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, List<AutoFormGroupConfiguration<TModel>>> SubGroups { get; set; } = [];
}