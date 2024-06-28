using NetEscapades.EnumGenerators;

namespace Blater.Frontend.Enumerations.AutoModel;

[EnumExtensions]
[Flags]
public enum AutoComponentDisplayType
{
    None = 0,
    FormCreate = 1,
    FormEdit = 2,
    Form = FormCreate | FormEdit,
    Details = 4,
    DataGrid = 8,
    Filter = 16
}