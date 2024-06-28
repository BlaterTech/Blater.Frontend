using System.Reflection;
using System.Text;
using Blater.Frontend.Enumerations.AutoModel;

// ReSharper disable CollectionNeverQueried.Global

namespace Blater.Frontend.AutoModelConfigurations.Configs;

public class AutoComponentConfiguration
{
    public readonly Dictionary<AutoComponentDisplayType, BaseAutoComponentTypeEnumeration> AutoComponentTypes = new();
    
    /// <summary>
    ///     Used for metadata
    /// </summary>
    public readonly Dictionary<string, object> ExtraAttributes = new();
    
    public readonly Dictionary<AutoComponentDisplayType, AutoGridConfiguration> Grids = new();
    
    public readonly Dictionary<AutoComponentDisplayType, int> Order = new();
    
    public readonly Dictionary<AutoComponentDisplayType, AutoFieldSize> Sizes = new();
    public PropertyInfo? Property { get; internal set; }
    
    public bool SeparateCard { get; set; }
    
    //Specific configurations
    
    /// <summary>
    ///     Used to set the field as important and hide the non-important fields
    /// </summary>
    public bool Important { get; set; }
    
    public bool SeparateComponent { get; set; }
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"PropName:({Property?.Name})");
        sb.AppendLine($"Order:({string.Join(',', Order.Select(x => $"{x.Key}:{x.Value}"))})");
        sb.AppendLine($"Sizes:({string.Join(',', Sizes.Select(x => $"{x.Key}:{x.Value}"))})");
        sb.AppendLine($"AutoComponentTypes:({string.Join(',', AutoComponentTypes.Select(x => $"{x.Key}:{x.Value.Name}"))})");
        sb.AppendLine($"GridsConfiguration:({string.Join(',', Grids.Select(x => $"{x.Key}:{x.Value}"))})");
        sb.AppendLine($"Important:({Important})");
        return sb.ToString();
    }
}