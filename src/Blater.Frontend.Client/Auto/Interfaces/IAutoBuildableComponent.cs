using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoBuildableComponent
{
    public BaseAutoComponentTypeEnumeration ComponentType { get; }
}