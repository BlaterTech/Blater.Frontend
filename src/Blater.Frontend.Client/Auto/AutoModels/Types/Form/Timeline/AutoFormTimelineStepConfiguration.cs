using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineStepConfiguration<TModel>(string title) : BaseAutoGroupConfiguration(title)
{
    public int Key { get; set; }
    public AutoFormConfiguration<TModel> AutoFormConfiguration { get; set; } = new("");
}