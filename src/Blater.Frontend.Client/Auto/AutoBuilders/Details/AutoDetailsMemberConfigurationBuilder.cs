using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, AutoGroupConfiguration configuration)
{
    public AutoDetailsPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentComponentConfiguration = new AutoComponentConfiguration
        {
            Property = type.GetProperty(propertyName)!
        };

        var containsConfiguration = configuration.Configurations.ContainsKey(AutoComponentDisplayType.Details);
        if (!containsConfiguration)
        {
            configuration.Configurations[AutoComponentDisplayType.Details] = [];
        }

        configuration.Configurations[AutoComponentDisplayType.Details].Add(currentComponentConfiguration);
        
        return new AutoDetailsPropertyConfigurationBuilder<TProperty>(currentComponentConfiguration);
    }
}