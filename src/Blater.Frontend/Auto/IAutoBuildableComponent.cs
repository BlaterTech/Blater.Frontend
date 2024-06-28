using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations.AutoModel;

namespace Blater.FrontEnd.Auto;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public interface IAutoBuildableComponent
{
    public BaseAutoComponentTypeEnumeration ComponentType { get; }

    //TODO Task BeforeSave();
}