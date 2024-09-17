using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, AutoDetailsGroupConfiguration configuration) : IAutoDetailsMemberConfigurationBuilder
{
    public IAutoDetailsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsAutoPropertyConfiguration propertyConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);

        var index = configuration.Components.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            configuration.Components[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = property;
            propertyConfiguration.AutoComponentType ??= property.GetDefaultComponentForType();
            configuration.Components.Add(propertyConfiguration);
        }

        return this;
    }
}