using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsGroupConfigurationBuilder<TModel>(AutoDetailsTabsPanelConfiguration<TModel> configuration) : IAutoDetailsTabsGroupConfigurationBuilder<TModel>
{
    public AutoDetailsTabsGroupConfiguration<TModel> AddGroup(string title, Action<IAutoDetailsTabsPropertyConfigurationBuilder<TModel>> memberConfiguration)
        => AddGroup(new AutoDetailsTabsGroupConfiguration<TModel>(title), memberConfiguration);

    public AutoDetailsTabsGroupConfiguration<TModel> AddGroup(string title, string route, Action<IAutoDetailsTabsPropertyConfigurationBuilder<TModel>> memberConfiguration)
        => AddGroup(new AutoDetailsTabsGroupConfiguration<TModel>(title, route), memberConfiguration);

    public AutoDetailsTabsGroupConfiguration<TModel> AddGroup(AutoDetailsTabsGroupConfiguration<TModel> groupConfiguration, Action<IAutoDetailsTabsPropertyConfigurationBuilder<TModel>> memberConfiguration)
    {
        var index = configuration.Groups.IndexOf(groupConfiguration);
        if (index != -1)
        {
            configuration.Groups[index] = groupConfiguration;
        }
        else
        {
            groupConfiguration.LocalizationId ??= $"Blater-AutoDetailsTabs-{typeof(TModel).Name}-Group";
            if (string.IsNullOrWhiteSpace(groupConfiguration.Title))
            {
                throw new Exception("Details tabs group title is null or white space");
            }
            configuration.Groups.Add(groupConfiguration);
        }
        
        var builder = new AutoDetailsTabsPropertyConfigurationBuilder<TModel>(groupConfiguration);
        
        memberConfiguration.Invoke(builder);

        return groupConfiguration;
    }
}