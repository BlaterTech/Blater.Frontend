using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormConfiguration
{
    AutoFormConfiguration FormConfiguration { get; }
    void Configure(AutoFormConfigurationBuilder builder);
}