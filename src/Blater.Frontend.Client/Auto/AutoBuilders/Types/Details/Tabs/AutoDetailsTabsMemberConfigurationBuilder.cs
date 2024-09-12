using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsMemberConfigurationBuilder(Type type, AutoDetailsTabsGroupConfiguration configuration) : IAutoDetailsTabsMemberConfigurationBuilder
{
    public IAutoDetailsTabsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsTabsComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var index = configuration.Components.IndexOf(componentConfiguration);
        if (index != -1)
        {
            configuration.Components[index] = componentConfiguration;
        }
        else
        {
            componentConfiguration.Property = property;
            componentConfiguration.AutoComponentType ??= property.GetDefaultAutoDetailsComponentForType();
            configuration.Components.Add(componentConfiguration);
        }

        return this;
    }   
}