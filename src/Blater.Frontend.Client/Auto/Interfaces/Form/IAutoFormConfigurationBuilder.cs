using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.Interfaces.Form;

public interface IAutoFormConfigurationBuilder<TModel>
{
    AutoFormModelConfiguration<TModel> Configuration { get; set; }
    IAutoFormConfigurationBuilder<TModel> Configure(string? title, Action<AutoFormGroupConfigurationBuilder<TModel>> action);
}