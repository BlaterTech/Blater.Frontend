using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsGroupConfigurationBuilder(Type type, DetailsGroupConfiguration configuration)
{
    public AutoDetailsPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new DetailsPropertyConfiguration
        {
            Property = type.GetProperty(propertyName)!
        };
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);

        return new AutoDetailsPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
    }
}