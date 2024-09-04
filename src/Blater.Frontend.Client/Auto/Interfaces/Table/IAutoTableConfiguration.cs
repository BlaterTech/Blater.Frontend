using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Table;

namespace Blater.Frontend.Client.Auto.Interfaces.Table;

public interface IAutoTableConfiguration
{
    AutoTableConfiguration Configuration { get; set; }
    void Configure(AutoTableConfigurationBuilder builder);
}