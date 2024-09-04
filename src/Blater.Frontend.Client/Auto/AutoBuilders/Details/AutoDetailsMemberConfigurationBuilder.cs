using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, AutoDetailsGroupConfiguration configuration)
{
    public AutoDetailsComponentConfigurationBuilder<TType> AddMember<TType>(Expression<Func<TType>> expression)
    {
        var property = expression.GetPropertyInfoForType(type);

        var currentDetailsComponentConfiguration = new AutoDetailsAutoComponentConfiguration
        {
            Property = property,
            AutoComponentType = property.GetDefaultAutoDetailsComponentForType()
        };

        configuration.Configurations.Add(currentDetailsComponentConfiguration);
        
        return new AutoDetailsComponentConfigurationBuilder<TType>(currentDetailsComponentConfiguration);
    }
}