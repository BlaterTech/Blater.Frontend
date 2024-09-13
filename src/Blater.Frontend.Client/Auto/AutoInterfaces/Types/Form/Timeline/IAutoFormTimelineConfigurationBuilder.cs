using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfigurationBuilder
{
    IAutoFormTimelineConfigurationBuilder AddStepCreateOnly(AutoFormTimelineStepConfiguration configuration);
    IAutoFormTimelineConfigurationBuilder AddStepEditOnly(AutoFormTimelineStepConfiguration configuration);
    IAutoFormTimelineConfigurationBuilder AddStep(AutoFormTimelineStepConfiguration configuration);
}