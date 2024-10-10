using NetEscapades.EnumGenerators;

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Blater.Frontend.Enumerations;

[EnumExtensions]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BlobAccessLevelType
{
    [Description("AzureStoragePublic")]
    Public,
    [Description("AzureStorage")]
    Private
}