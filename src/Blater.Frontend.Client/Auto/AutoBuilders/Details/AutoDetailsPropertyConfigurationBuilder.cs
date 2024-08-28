using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsPropertyConfigurationBuilder<TProperty>(DetailsPropertyConfiguration configuration)
    : AutoPropertyConfigurationBuilder(configuration)
{
    
    public AutoDetailsPropertyConfigurationBuilder<TProperty> ComponentType(string value)
    {
        configuration.ComponentType = value;
        return this;
    }
}