using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsMemberConfigurationBuilder(Type type, AutoDetailsTabsGroupConfiguration configuration) : IAutoDetailsTabsMemberConfigurationBuilder
{
    public IAutoDetailsTabsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsTabsPropertyConfiguration propertyConfiguration)
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