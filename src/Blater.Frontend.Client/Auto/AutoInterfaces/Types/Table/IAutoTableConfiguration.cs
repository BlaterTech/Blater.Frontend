using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Table;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableConfiguration
{
    AutoTableConfiguration TableConfiguration { get; }
    void Configure(AutoTableConfigurationBuilder builder);
}