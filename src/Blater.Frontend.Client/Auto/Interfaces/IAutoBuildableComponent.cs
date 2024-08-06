using Blater.Frontend.Client.Auto.AutoModels;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoBuildableComponent
{
    public BaseAutoComponentTypeEnumeration ComponentType { get; }
}