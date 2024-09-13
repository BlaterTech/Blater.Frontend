using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineConfigurationBuilder : IAutoFormTimelineConfigurationBuilder
{
    private readonly AutoFormTimelineConfiguration _configuration;
    private readonly Type _type;

    public AutoFormTimelineConfigurationBuilder(Type type, object instance)
    {
        _type = type;
        if (instance is IAutoFormTimelineConfiguration configuration)
        {
            _configuration = configuration.FormTimelineConfiguration;
        }
        else
        {
            throw new InvalidCastException("Instance is not implement IAutoFormTimelineConfiguration");
        }
    }

    public IAutoFormTimelineConfigurationBuilder AddStepCreateOnly(AutoFormTimelineStepConfiguration configuration)
        => AddStep(AutoComponentDisplayType.FormTimelineCreate, configuration);
    
    public IAutoFormTimelineConfigurationBuilder AddStepEditOnly(AutoFormTimelineStepConfiguration configuration)
        => AddStep(AutoComponentDisplayType.FormTimelineEdit, configuration);

    public IAutoFormTimelineConfigurationBuilder AddStep(AutoFormTimelineStepConfiguration configuration)
        => AddStep(AutoComponentDisplayType.FormTimeline, configuration);
    
    private AutoFormTimelineConfigurationBuilder AddStep(AutoComponentDisplayType displayType, AutoFormTimelineStepConfiguration configuration)
    {
        if (!_configuration.Steps.TryGetValue(displayType, out var value))
        {
            value ??= [];
            _configuration.Steps.TryAdd(displayType, value);
        }

        var index = value.IndexOf(configuration);
        if (index != -1)
        {
            value[index] = configuration;
        }
        else
        {
            value.Add(configuration);
        }

        _configuration.Steps[displayType] = value;

        return this;
    }
}