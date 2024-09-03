using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.Interfaces.Form;

public interface IAutoFormConfigurationBuilder<TModel>
{
    IAutoFormConfigurationBuilder<TModel> Configure(AutoFormModelConfiguration<TModel> model);
    IAutoFormConfigurationBuilder<TModel> Configure(string? title, Action<AutoFormGroupConfigurationBuilder<TModel>> action);
}