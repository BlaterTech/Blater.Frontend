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
    Details = 4,
    Table = 8,
    Filter = 16
}