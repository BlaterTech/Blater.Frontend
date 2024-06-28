using Ardalis.SmartEnum;

namespace Blater.Frontend.Enumerations.AutoModel;

public class BaseAutoComponentTypeEnumeration(string name, int value) : SmartEnum<BaseAutoComponentTypeEnumeration>(name, value)
{
    public virtual bool HasValueChanged { get; set; }
}