using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfigurationBuilder<TModel>
{
    AutoFormTimelineConfiguration AddStepCreateOnly(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action);
    AutoFormTimelineConfiguration AddStepEditOnly(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action);
    AutoFormTimelineConfiguration AddStep(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action);
}