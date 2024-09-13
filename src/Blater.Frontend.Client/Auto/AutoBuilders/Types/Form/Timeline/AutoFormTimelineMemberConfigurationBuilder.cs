using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineMemberConfigurationBuilder(Type type, AutoFormTimelineGroupConfiguration configuration) : IAutoFormTimelineMemberConfigurationBuilder
{
    public IAutoFormTimelineMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoFormAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var index = configuration.ComponentConfigurations.IndexOf(componentConfiguration);
        if (index != -1)
        {
            configuration.ComponentConfigurations[index] = componentConfiguration;
        }
        else
        {
            componentConfiguration.Property = property;
            componentConfiguration.AutoComponentType ??= property.GetDefaultComponentForType();
            configuration.ComponentConfigurations.Add(componentConfiguration);
        }

        return this;
    }
}