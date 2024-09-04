using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.Table;

public interface IAutoTableConfiguration
{
    void ConfigureTable(AutoTableConfigurationBuilder builder);
}