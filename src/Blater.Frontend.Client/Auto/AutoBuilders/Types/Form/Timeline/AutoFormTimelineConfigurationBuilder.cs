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

    public IAutoFormTimelineConfigurationBuilder AddStepCreateOnly(string title)
        => AddStep(AutoComponentDisplayType.FormTimelineCreate, title);
    
    public IAutoFormTimelineConfigurationBuilder AddStepEditOnly(string title)
        => AddStep(AutoComponentDisplayType.FormTimelineEdit, title);

    public IAutoFormTimelineConfigurationBuilder AddStep(string title)
        => AddStep(AutoComponentDisplayType.FormTimeline, title);
    
    private AutoFormTimelineConfigurationBuilder AddStep(AutoComponentDisplayType displayType, string title)
    {
        if (!_configuration.Steps.TryGetValue(displayType, out var value))
        {
            value ??= [];
            _configuration.Steps.TryAdd(displayType, value);
        }

        var containsValue = value.ContainsValue(title);
        if (!containsValue)
        {
            var (lastKey, lastValue) = value.LastOrDefault();

            int key;
            if (string.IsNullOrWhiteSpace(lastValue))
            {
                key = 0;
            }
            else
            {
                key = lastKey + 1;
            }
            
            value.TryAdd(key, title);
        }

        _configuration.Steps[displayType] = value;

        return this;
    }
}