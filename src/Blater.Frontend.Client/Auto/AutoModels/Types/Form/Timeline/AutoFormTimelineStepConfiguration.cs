namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

public class AutoFormTimelineStepConfiguration
{
    public int Step { get; private set; }
    public string Title { get; private set; } = null!;

    public void Add(int step, string title)
    {
        Step = step;
        Title = title;
    }
}