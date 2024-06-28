// ReSharper disable InconsistentNaming

using System.Text.Json.Serialization;
using Blater.Frontend.Attributes;

namespace Blater.Frontend.Enumerations;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BlaterProjects
{
    None = 0,
    
    [ShortName("as")]
    StaticFiles = 1,
    
    [ShortName("mp")]
    Medpoint = 1001,
    
    [ShortName("lp")]
    Lipor = 1002,
    
    [ShortName("js")]
    Juriself = 1003
}