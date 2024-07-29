using System.Text.Json.Serialization;
using Blater.Attributes;

namespace Blater.Frontend.Enumerations;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BlaterProjects
{
    None = 0,
}