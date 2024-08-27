using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Models.Bases;

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
            PropertyInfo = type.GetProperty(propertyName)!
        };
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);

        return new AutoDetailsPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
    }
}