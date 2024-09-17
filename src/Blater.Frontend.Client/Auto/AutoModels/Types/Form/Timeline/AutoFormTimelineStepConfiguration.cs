namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineStepConfiguration
{
    public int Key { get; set; }
    public required string Value { get; set; }
    public AutoFormConfiguration AutoFormConfiguration { get; set; } = null!;
}