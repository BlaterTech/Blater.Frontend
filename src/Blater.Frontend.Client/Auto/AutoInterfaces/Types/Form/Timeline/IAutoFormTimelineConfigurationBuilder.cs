using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfigurationBuilder
{
    IAutoFormTimelineStepConfigurationBuilder AddStepCreateOnly(string title);
    IAutoFormTimelineStepConfigurationBuilder AddStepEditOnly(string title);
    IAutoFormTimelineStepConfigurationBuilder AddStep(string title);
}

public interface IAutoFormTimelineStepConfigurationBuilder : IAutoFormConfigurationBuilder
{
    
}