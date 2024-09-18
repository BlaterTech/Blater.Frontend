using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsGroupConfigurationBuilder<TModel>
{
    AutoDetailsTabsGroupConfiguration<TModel> AddGroup(string title, Action<IAutoDetailsTabsPropertyConfigurationBuilder<TModel>> memberConfiguration);
    AutoDetailsTabsGroupConfiguration<TModel> AddGroup(string title, string route, Action<IAutoDetailsTabsPropertyConfigurationBuilder<TModel>> memberConfiguration);
    AutoDetailsTabsGroupConfiguration<TModel> AddGroup(AutoDetailsTabsGroupConfiguration<TModel> groupConfiguration, Action<IAutoDetailsTabsPropertyConfigurationBuilder<TModel>> memberConfiguration);
}