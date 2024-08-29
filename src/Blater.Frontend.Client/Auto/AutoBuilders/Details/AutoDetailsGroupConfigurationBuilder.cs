using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsGroupConfigurationBuilder(Type type, AutoGroupConfiguration configuration)
{
    public AutoDetailsMemberConfigurationBuilder AddGroup(string groupName, bool disableEditButton, Action<AutoDetailsGroupConfigurationBuilder> action)
    {
        var currentGroupConfiguration = new DetailsGroupConfiguration
        {
            Title = groupName,
            DisableEditButton = disableEditButton
        };

        var groupsConfigurations = configuration.GroupsConfigurations ??= new List<DetailsGroupConfiguration>();
        groupsConfigurations.Add(currentGroupConfiguration);

        var autoDetailsGroupConfigurationBuilder = new AutoDetailsGroupConfigurationBuilder(type, currentGroupConfiguration);

        action(autoDetailsGroupConfigurationBuilder);
        
        return this;
    }
}