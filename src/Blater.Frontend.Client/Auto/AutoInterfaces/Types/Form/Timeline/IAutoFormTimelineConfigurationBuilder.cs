using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfigurationBuilder<TModel>
{
    AutoFormTimelineConfiguration<TModel> AddStepCreateOnly(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action);
    AutoFormTimelineConfiguration<TModel> AddStepEditOnly(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action);
    AutoFormTimelineConfiguration<TModel> AddStep(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action);
}