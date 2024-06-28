using System.Text.Json.Serialization;
using NetEscapades.EnumGenerators;

namespace Blater.Frontend.Enumerations;

[EnumExtensions]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Environments
{
    Local,
    Test,
    Production
}