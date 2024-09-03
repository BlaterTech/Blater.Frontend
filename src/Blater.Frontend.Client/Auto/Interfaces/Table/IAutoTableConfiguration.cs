using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.Table;

public interface IAutoTableConfiguration<T> where T : BaseDataModel
{
    void ConfigureTable(AutoTableConfigurationBuilder<T> builder);
}