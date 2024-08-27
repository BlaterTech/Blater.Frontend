using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Models.Bases;

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