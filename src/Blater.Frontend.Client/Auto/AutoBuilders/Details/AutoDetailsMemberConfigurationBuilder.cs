using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, DetailsGroupConfiguration configuration)
{
    public AutoDetailsComponentConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentDetailsComponentConfiguration = new DetailsComponentConfiguration
        {
            Property = type.GetProperty(propertyName)!,
            AutoComponentTypes =
            {
                [AutoComponentDisplayType.Details] = type.GetDefaultAutoDetailsComponentForType()
            }
        };

        configuration.Configurations.Add(currentDetailsComponentConfiguration);
        
        return new AutoDetailsComponentConfigurationBuilder<TProperty>(currentDetailsComponentConfiguration);
    }
}