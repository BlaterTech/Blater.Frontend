using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineConfiguration(string title)
{
    public string Title { get; set; } = title;

    public Dictionary<AutoComponentDisplayType, List<AutoFormTimelineStepConfiguration>> Steps { get; set; } = [];
}