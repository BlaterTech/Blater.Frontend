using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoGridConfiguration
{
    public int Columns { get; set; } = 2;
    public int Spacing { get; set; } = 2;
    public int Rows { get; set; }

    public AutoGridType GridType { get; set; } = AutoGridType.Normal;

    public AutoFlexType FlexType { get; set; } = AutoFlexType.Normal;
}