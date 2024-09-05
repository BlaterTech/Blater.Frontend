using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.Interfaces.Types.Details;

public interface IAutoDetailsConfiguration
{
    AutoDetailsConfiguration DetailsConfiguration { get; }
    void Configure(AutoDetailsConfigurationBuilder builder);
}