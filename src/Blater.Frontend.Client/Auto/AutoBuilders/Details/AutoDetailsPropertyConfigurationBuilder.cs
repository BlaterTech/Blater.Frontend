using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsPropertyConfigurationBuilder<T, TProperty>(DetailsPropertyConfiguration configuration)
    : AutoPropertyConfigurationBuilder<T>(configuration)
    where T : BaseDataModel
{
    
    public AutoDetailsPropertyConfigurationBuilder<T, TProperty> ComponentType(string value)
    {
        configuration.ComponentType = value;
        return this;
    }
    
}