using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoInterfaces;

public interface IAutoBuildableComponent
{
    public BaseAutoComponentTypeEnumeration ComponentType { get; }
}