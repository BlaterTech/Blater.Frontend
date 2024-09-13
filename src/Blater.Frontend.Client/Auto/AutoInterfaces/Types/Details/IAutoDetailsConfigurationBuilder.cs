using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

public interface IAutoDetailsConfigurationBuilder
{
    IAutoDetailsConfigurationBuilder AddGroup(AutoDetailsGroupConfiguration detailsGroupConfiguration, Action<IAutoDetailsMemberConfigurationBuilder> action);
}