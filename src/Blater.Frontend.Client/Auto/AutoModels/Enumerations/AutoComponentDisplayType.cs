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
    Table = 4,
    Details = 8
}