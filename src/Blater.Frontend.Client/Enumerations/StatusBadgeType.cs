using System.Text.Json.Serialization;
using NetEscapades.EnumGenerators;

namespace Blater.Frontend.Client.Enumerations;

[EnumExtensions]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusBadgeType
{
    Unknown = 0,
    Available = 1,
    Loading = 2,
    Processing = 3,
    InUse = 4,
    Active = 5,
    Overdue = 6,
    Pending = 7,
    InProgress = 8,
    Concluded = 9
}