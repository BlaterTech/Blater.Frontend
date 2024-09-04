using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsConfigurationBuilder(Type type, AutoDetailsConfiguration configuration)
{
    public AutoDetailsMemberConfigurationBuilder AddGroup(AutoDetailsGroupConfiguration detailsGroupConfiguration)
    {
        var index = configuration.Configurations.IndexOf(detailsGroupConfiguration);
        if (index != -1)
        {
            configuration.Configurations[index] = detailsGroupConfiguration;
            return new AutoDetailsMemberConfigurationBuilder(type, detailsGroupConfiguration);
        }
        
        configuration.Configurations.Add(detailsGroupConfiguration);
        return new AutoDetailsMemberConfigurationBuilder(type, detailsGroupConfiguration);
    }
}