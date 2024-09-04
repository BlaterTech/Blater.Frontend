using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.Details;

public interface IAutoDetailsConfiguration
{
    void ConfigureDetails(AutoDetailsConfigurationBuilder builder);
}