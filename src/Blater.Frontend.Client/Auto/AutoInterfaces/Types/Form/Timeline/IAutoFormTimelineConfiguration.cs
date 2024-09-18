using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineConfiguration<TModel>
{
    AutoFormTimelineConfiguration <TModel>FormTimelineConfiguration { get; }
    void ConfigureFormTimeline(IAutoFormTimelineConfigurationBuilder<TModel> builder);
}