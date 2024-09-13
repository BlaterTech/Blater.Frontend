using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfiguration : IAutoFormConfiguration
{
    AutoFormTimelineConfiguration FormTimelineConfiguration { get; }
    void Configure(IAutoFormTimelineConfigurationBuilder builder);
}