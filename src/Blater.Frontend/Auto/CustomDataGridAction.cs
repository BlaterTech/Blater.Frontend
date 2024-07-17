using Blater.Models.Bases;
using MudBlazor;

namespace Blater.Frontend.Auto;


public class CustomDataGridAction<T> where T : BaseDataModel
{
    public Action<T> CallbackAction { get; set; } = null!;
    public Color? Color { get; set; }
    public string Icon { get; set; } = null!;
    public string Tooltip { get; set; } = null!;
}