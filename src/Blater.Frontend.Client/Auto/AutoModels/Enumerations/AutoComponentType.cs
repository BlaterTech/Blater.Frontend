using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Enumerations;

public class AutoComponentType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoComponentType ImageRectangle = new(nameof(ImageRectangle), 201);
    public static readonly AutoComponentType ImageRounded = new(nameof(ImageRounded), 202);
    public static readonly AutoComponentType ImageCircle = new(nameof(ImageCircle), 203);
    public static readonly AutoComponentType AutoBadge = new(nameof(AutoBadge), 205);
    public static readonly AutoComponentType AutoShortId = new(nameof(AutoShortId), 206);
    public static readonly AutoComponentType AutoMoney = new(nameof(AutoMoney), 207);
    public static readonly AutoComponentType AutoImageCircle = new(nameof(AutoImageCircle), 208);
    public static readonly AutoComponentType AutoImageRectangle = new(nameof(AutoImageRectangle), 209);
    public static readonly AutoComponentType AutoImageRounded = new(nameof(AutoImageRounded), 210);
    public static readonly AutoComponentType AutoDate = new(nameof(AutoDate), 211);
    public static readonly AutoComponentType AutoText = new(nameof(AutoText), 212);
    public static readonly AutoComponentType AutoMoneyRounded = new(nameof(AutoMoneyRounded), 213);
    public static readonly AutoComponentType AutoCNPJFormatted = new(nameof(AutoCNPJFormatted), 214);
    public static readonly AutoComponentType AutoStatus = new(nameof(AutoStatus), 215);
    public static readonly AutoComponentType AutoTextArea = new(nameof(AutoTextArea), 216);
    public static readonly AutoComponentType AutoTextStatus = new(nameof(AutoTextStatus), 217);
    
    public static readonly AutoComponentType AutoTextTable = new(nameof(AutoTextTable), 218);
    
    /*public static readonly AutoComponentType AutoDateTable = new(nameof(AutoDateTable), 219);*/
    public static readonly AutoComponentType AutoWallet = new(nameof(AutoWallet), 220);
    public static readonly AutoComponentType AutoWalletTable = new(nameof(AutoWalletTable), 221);
    public static readonly AutoComponentType AutoAddress = new(nameof(AutoAddress), 222);
    public static readonly AutoComponentType AutoImageRectangleDetails = new(nameof(AutoImageRectangleDetails), 223);
    public static readonly AutoComponentType AutoMoneyRoundedTable = new(nameof(AutoMoneyRoundedTable), 224);
    public static readonly AutoComponentType AutoLastCreditCardNumbers = new(nameof(AutoLastCreditCardNumbers), 225);
    public static readonly AutoComponentType AutoUserGroupName = new(nameof(AutoUserGroupName), 226);
    public static readonly AutoComponentType AutoDecimal = new(nameof(AutoDecimal), 227);
    public static readonly AutoComponentType AutoTextLink = new(nameof(AutoTextLink), 228);
    
    
    public static readonly AutoComponentType AutoFileDetails = new(nameof(AutoFileDetails), 229);
    public static readonly AutoComponentType AutoSectionText = new(nameof(AutoSectionText), 230);
    public static readonly AutoComponentType AutoNumeric = new(nameof(AutoNumeric), 231);
    public static readonly AutoComponentType AutoGuidListCount = new(nameof(AutoGuidListCount), 232);
    public static readonly AutoComponentType AutoDataGrid = new(nameof(AutoDataGrid), 233);
    public static readonly AutoComponentType AutoTextEnum = new(nameof(AutoTextEnum), 234);
    public static readonly AutoComponentType AutoTitle = new(nameof(AutoTitle), 235);
    
    public static readonly AutoComponentType AutoMPPT = new(nameof(AutoMPPT), 236);
    public static readonly AutoComponentType AutoLiporProjectStatusEnum = new(nameof(AutoLiporProjectStatusEnum), 237);
    public override bool HasValueChanged { get; set; } = false;
}