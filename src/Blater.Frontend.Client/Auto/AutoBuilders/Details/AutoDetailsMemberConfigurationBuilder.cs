using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, AutoDetailsGroupConfiguration configuration)
{
    public AutoDetailsComponentConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        var property = expression.GetPropertyInfoForType(type);

        var currentDetailsComponentConfiguration = new AutoDetailsComponentConfiguration
        {
            Property = property,
            AutoComponentTypes =
            {
                [AutoComponentDisplayType.Details] = property.GetDefaultAutoDetailsComponentForType()
            }
        };

        configuration.Configurations.Add(currentDetailsComponentConfiguration);
        
        return new AutoDetailsComponentConfigurationBuilder<TProperty>(currentDetailsComponentConfiguration);
    }
}