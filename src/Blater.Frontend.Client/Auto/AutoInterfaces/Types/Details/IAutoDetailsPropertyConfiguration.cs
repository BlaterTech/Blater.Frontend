using Blater.Frontend.Client.Auto.AutoInterfaces.Base;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

public interface IAutoDetailsPropertyConfiguration<TModel> 
    : IBaseAutoPropertyConfiguration
{
    public bool SeparateCard { get; set; }
    public bool SeparateComponent { get; set; }
}