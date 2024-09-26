using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineConfigurationBuilder<TModel> : IAutoFormTimelineConfigurationBuilder<TModel>
{
    private readonly AutoFormTimelineConfiguration<TModel> _configuration;

    public AutoFormTimelineConfigurationBuilder(object instance)
    {
        if (instance is IAutoFormTimelineConfiguration<TModel> configuration)
        {
            _configuration = configuration.FormTimelineConfiguration;
            _configuration.LocalizationId ??= $"Blater-AutoFormTimeline-{typeof(TModel).Name}";
        }
        else
        {
            throw new InvalidCastException($"Instance is not implement IAutoFormTimelineConfiguration<{typeof(TModel).Name}>");
        }
    }

    public AutoFormTimelineConfiguration<TModel> AddStepCreateOnly(string title, Action<IAutoFormTimelineFormConfigurationBuilder<TModel>> action)
        => AddStep(title, AutoComponentDisplayType.FormTimelineCreate, action);

    public AutoFormTimelineConfiguration<TModel> AddStepEditOnly(string title, Action<IAutoFormTimelineFormConfigurationBuilder<TModel>> action)
        => AddStep(title, AutoComponentDisplayType.FormTimelineCreate, action);

    public AutoFormTimelineConfiguration<TModel> AddStep(string title, Action<IAutoFormTimelineFormConfigurationBuilder<TModel>> action)
        => AddStep(title, AutoComponentDisplayType.FormTimeline, action);

    private AutoFormTimelineConfiguration<TModel> AddStep(string title, AutoComponentDisplayType displayType, Action<IAutoFormTimelineFormConfigurationBuilder<TModel>> action)
    {
        if (!_configuration.Steps.TryGetValue(displayType, out var value))
        {
            value ??= [];
            _configuration.Steps.TryAdd(displayType, value);
        }

        var item = value.LastOrDefault();

        int step;
        if (string.IsNullOrWhiteSpace(item?.Title))
        {
            step = 1;
        }
        else
        {
            step = item.Key + 1;
        }

        var newStep = new AutoFormTimelineStepConfiguration<TModel>(title)
        {
            LocalizationId = $"Blater-AutoFormTimeline-{typeof(TModel).Name}-{title}",
            Key = step,
            AutoFormConfiguration = new AutoFormConfiguration<TModel>("")
        };

        value.Add(newStep);

        _configuration.Steps[displayType] = value;
        
        var builder = new AutoFormTimelineFormConfigurationBuilder<TModel>(newStep.AutoFormConfiguration);

        action.Invoke(builder);

        return _configuration;
    }
}