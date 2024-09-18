using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTableConfiguration<TModel>
{
    AutoTableConfiguration<TModel> TableConfiguration { get; set; }
    void ConfigureTable(AutoTableConfigurationBuilder<TModel> builder);
}