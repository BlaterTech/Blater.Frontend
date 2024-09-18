using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormConfiguration<TModel>
{
    AutoFormConfiguration<TModel> FormConfiguration { get; set; }
    void ConfigureForm(AutoFormConfigurationBuilder<TModel> builder);
}