using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.Form;

public interface IAutoFormConfiguration<T> where T : BaseDataModel
{
    void ConfigureForm(AutoFormConfigurationBuilder<T> builder);
    
    AutoFormModelConfiguration<T> Configuration { get; }
}