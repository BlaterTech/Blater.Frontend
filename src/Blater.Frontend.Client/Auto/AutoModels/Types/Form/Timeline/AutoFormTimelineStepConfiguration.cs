namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineStepConfiguration(int step, string title)
{
    public int Step { get; set; } = step;
    public string Title { get; set; } = title;
}