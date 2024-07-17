using Blater.Enumerations.AutoModel;
using Blater.Models.Bases;

namespace Blater.Frontend.Auto.AutoComponents;


public partial class AutoDataGridDetails<TIem, TList> 
    where TIem : BaseDataModel
    where TList : List<TIem>
{
    public int Priority => 1000;

    public BaseAutoComponentTypeEnumeration ComponentType => AutoComponentType.AutoDataGrid;
}