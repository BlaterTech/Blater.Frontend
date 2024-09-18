using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineConfiguration<TModel>(string title)
{
    public string Title { get; set; } = title;
    
    public Dictionary<AutoComponentDisplayType, List<AutoFormTimelineStepConfiguration<TModel>>> Steps { get; set; } = [];
}