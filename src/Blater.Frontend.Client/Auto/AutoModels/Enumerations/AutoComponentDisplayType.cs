using NetEscapades.EnumGenerators;

namespace Blater.Frontend.Client.Auto.AutoModels.Enumerations;

[EnumExtensions]
[Flags]
public enum AutoComponentDisplayType
{
    None = 0,
    FormCreate = 1,
    FormEdit = 2,
    Form = FormCreate | FormEdit,
    FormTimelineCreate = 4,
    FormTimelineEdit = 8,
    FormTimeline = FormTimelineCreate | FormTimelineEdit,
    Table = 16,
    Details = 32,
    DetailsTabs = 64
}