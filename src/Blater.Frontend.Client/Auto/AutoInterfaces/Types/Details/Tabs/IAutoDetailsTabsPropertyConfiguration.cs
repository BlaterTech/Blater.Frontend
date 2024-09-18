using Blater.Frontend.Client.Auto.AutoInterfaces.Base;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;

public interface IAutoDetailsTabsPropertyConfiguration<TModel> : IBaseAutoPropertyConfiguration
{
    public string? HeadTitle { get; set; }
}