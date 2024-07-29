using System.Text.Json.Serialization;

namespace Blater.Frontend.Enumerations;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BlaterProjects
{
    None = 0,
}