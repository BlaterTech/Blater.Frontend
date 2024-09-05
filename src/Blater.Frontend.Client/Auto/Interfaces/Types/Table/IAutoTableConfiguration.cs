using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Table;

namespace Blater.Frontend.Client.Auto.Interfaces.Types.Table;

public interface IAutoTableConfiguration
{
    AutoTableConfiguration TableConfiguration { get; }
    void Configure(AutoTableConfigurationBuilder builder);
}