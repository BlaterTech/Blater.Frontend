using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.Interfaces.Form;

public interface IAutoFormConfiguration
{
    AutoFormConfiguration Configuration { get; set; }
    void Configure(AutoFormConfigurationBuilder builder);
}