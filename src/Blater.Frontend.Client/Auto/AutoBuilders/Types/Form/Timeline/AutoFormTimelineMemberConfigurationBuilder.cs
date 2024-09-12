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

        var currentComponentConfig = configuration.ComponentConfigurations.FirstOrDefault(x => x.Property == property);

        if (currentComponentConfig != null && currentComponentConfig.AutoComponentType == null)
        {
            currentComponentConfig = componentConfiguration;
            currentComponentConfig.AutoComponentType = property.GetDefaultAutoFormComponentForType();
        }
        else
        {
            currentComponentConfig = componentConfiguration;
            currentComponentConfig.Property = property;
            currentComponentConfig.AutoComponentType ??= property.GetDefaultAutoFormComponentForType();
            configuration.ComponentConfigurations.Add(currentComponentConfig);
        }

        return this;
    }
}