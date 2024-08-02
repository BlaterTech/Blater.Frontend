using Blater.Models.Bases;
using MudBlazor;

namespace Blater.Frontend.Client.Models;

public class CustomDataGridAction<T> where T : BaseDataModel
{
    public Action<T> OnValueChanged { get; set; } = null!;
    
    public Color? Color { get; set; }
    
    public string Icon { get; set; } = null!;
    
    public string Tooltip { get; set; } = null!;
}