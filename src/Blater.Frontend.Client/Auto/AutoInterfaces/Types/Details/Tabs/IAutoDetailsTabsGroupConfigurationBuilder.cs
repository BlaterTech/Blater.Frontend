using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsGroupConfigurationBuilder<TModel>
{
    AutoDetailsTabsGroupConfiguration AddGroup(AutoDetailsTabsGroupConfiguration groupConfiguration, Action<IAutoDetailsTabsMemberConfigurationBuilder<TModel>> memberConfiguration);
}