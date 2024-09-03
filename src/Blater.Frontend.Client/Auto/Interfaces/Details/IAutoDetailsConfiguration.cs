using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.Details;

public interface IAutoDetailsConfiguration<T> where T : BaseDataModel
{
    void ConfigureDetails(AutoDetailsConfigurationBuilder<T> builder);
}