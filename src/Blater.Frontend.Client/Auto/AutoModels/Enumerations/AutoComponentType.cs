using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Enumerations;

public class AutoComponentType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoComponentType AutoBadge = new(nameof(AutoBadge), 205);
    public static readonly AutoComponentType AutoDate = new(nameof(AutoDate), 211);
    public static readonly AutoComponentType AutoText = new(nameof(AutoText), 212);
    public static readonly AutoComponentType AutoStatus = new(nameof(AutoStatus), 215);
    
    public static readonly AutoComponentType AutoTextTable = new(nameof(AutoTextTable), 218);
    
    
    public static readonly AutoComponentType AutoFileDetails = new(nameof(AutoFileDetails), 229);
    public static readonly AutoComponentType AutoNumeric = new(nameof(AutoNumeric), 231);
    public static readonly AutoComponentType AutoTitle = new(nameof(AutoTitle), 235);
    public static readonly AutoComponentType AutoId = new(nameof(AutoId), 236);
    
    public override bool HasValueChanged => false;
}