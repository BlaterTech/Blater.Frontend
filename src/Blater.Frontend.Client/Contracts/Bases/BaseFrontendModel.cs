using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Contracts.Bases;

public abstract class BaseFrontendModel : BaseDataModel, IBaseAutoConfiguration
{
    public bool Enabled { get; set; }
}