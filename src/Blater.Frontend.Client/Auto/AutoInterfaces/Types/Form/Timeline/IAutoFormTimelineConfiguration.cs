using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfiguration
{
    AutoFormTimelineConfiguration FormTimelineConfiguration { get; }
    void ConfigureFormTimeline(IAutoFormTimelineConfigurationBuilder builder);
}