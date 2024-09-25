using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineConfiguration<TModel>(string title) : BaseAutoConfiguration(title)
{
    public Dictionary<AutoComponentDisplayType, List<AutoFormTimelineStepConfiguration<TModel>>> Steps { get; set; } = [];
}