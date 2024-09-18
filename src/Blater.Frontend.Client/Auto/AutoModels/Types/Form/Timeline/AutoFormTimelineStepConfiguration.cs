namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineStepConfiguration<TModel>
{
    public int Key { get; set; }
    public required string Value { get; set; }
    public AutoFormConfiguration<TModel> AutoFormConfiguration { get; set; } = null!;
}