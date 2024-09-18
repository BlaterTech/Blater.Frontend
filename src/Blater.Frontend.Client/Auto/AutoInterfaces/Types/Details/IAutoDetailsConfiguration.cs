using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

public interface IAutoDetailsConfiguration<TModel>
{
    AutoDetailsConfiguration<TModel> DetailsConfiguration { get; }
    void ConfigureDetails(AutoDetailsConfigurationBuilder<TModel> builder);
}