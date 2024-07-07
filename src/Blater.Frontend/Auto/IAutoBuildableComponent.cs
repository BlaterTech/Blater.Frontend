using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations.AutoModel;

namespace Blater.Frontend.Auto;


public interface IAutoBuildableComponent
{
    public BaseAutoComponentTypeEnumeration ComponentType { get; }

    //TODO Task BeforeSave();
}