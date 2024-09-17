using Blater.Frontend.Client.Auto.AutoInterfaces;
using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Models.Bases;

public abstract class BaseFrontendModel : BaseDataModel, IBaseAutoConfiguration
{
    public bool Enabled { get; set; }
}