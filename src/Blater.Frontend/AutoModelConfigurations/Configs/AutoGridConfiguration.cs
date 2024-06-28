using System.Text;
using Blater.Frontend.Enumerations.AutoModel;

namespace Blater.Frontend.AutoModelConfigurations.Configs;

public class AutoGridConfiguration
{
    public int Columns { get; set; } = 2;
    public int Rows { get; set; }
    
    public AutoGridType GridType { get; set; } = AutoGridType.Normal;
    
    public AutoFlexType FlexType { get; set; } = AutoFlexType.Normal;
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("Columns: "  + Columns);
        sb.AppendLine("Rows: "     + Rows);
        sb.AppendLine("GridType: " + GridType);
        sb.AppendLine("FlexType: " + FlexType);
        
        return sb.ToString();
    }
}