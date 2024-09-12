using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableConfiguration
{
    AutoTableConfiguration TableConfiguration { get; }
    void Configure(AutoTableConfigurationBuilder builder);
}