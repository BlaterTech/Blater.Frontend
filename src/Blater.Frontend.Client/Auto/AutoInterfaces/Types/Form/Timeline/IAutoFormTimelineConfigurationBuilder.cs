using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfigurationBuilder
{
    IAutoFormTimelineConfigurationBuilder AddStepCreateOnly(string title);
    IAutoFormTimelineConfigurationBuilder AddStepEditOnly(string title);
    IAutoFormTimelineConfigurationBuilder AddStep(string title);
}