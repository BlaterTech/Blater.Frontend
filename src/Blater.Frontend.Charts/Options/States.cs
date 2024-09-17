namespace Blater.Frontend.Charts.Options;

public class States
{
    public State Normal { get; set; } = new()
    {
        Filter = new Filter
        {
            Type = "none",
            Value = 0
        }
    };

    public State Hover { get; set; } = new()
    {
        Filter = new Filter
        {
            Type = "lighten",
            Value = 0.15
        }
    };

    public State Active { get; set; } = new()
    {
        AllowMultipleDataPointsSelection = false,
        Filter = new Filter
        {
            Type = "darken",
            Value = 0.35
        }
    };
}

public class Filter
{
    public string Type { get; set; } = "none";
    public double Value { get; set; }
}

public class State
{
    public Filter Filter { get; set; } = new();
    public bool AllowMultipleDataPointsSelection { get; set; }
}