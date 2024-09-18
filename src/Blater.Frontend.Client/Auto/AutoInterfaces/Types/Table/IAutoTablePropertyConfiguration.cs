using Blater.Frontend.Client.Auto.AutoInterfaces.Base;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;

public interface IAutoTablePropertyConfiguration<TModel> : IBaseAutoPropertyConfiguration
{
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
}