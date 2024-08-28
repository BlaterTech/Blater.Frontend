using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoDetailsComponentType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoDetailsComponentType ImageRectangle = new(nameof(ImageRectangle), 201);
    public static readonly AutoDetailsComponentType ImageRounded = new(nameof(ImageRounded), 202);
    public static readonly AutoDetailsComponentType ImageCircle = new(nameof(ImageCircle), 203);
    public static readonly AutoDetailsComponentType AutoBadge = new(nameof(AutoBadge), 205);
    public static readonly AutoDetailsComponentType AutoShortId = new(nameof(AutoShortId), 206);
    public static readonly AutoDetailsComponentType AutoMoney = new(nameof(AutoMoney), 207);
    public static readonly AutoDetailsComponentType AutoImageCircle = new(nameof(AutoImageCircle), 208);
    public static readonly AutoDetailsComponentType AutoImageRectangle = new(nameof(AutoImageRectangle), 209);
    public static readonly AutoDetailsComponentType AutoImageRounded = new(nameof(AutoImageRounded), 210);
    public static readonly AutoDetailsComponentType AutoDate = new(nameof(AutoDate), 211);
    public static readonly AutoDetailsComponentType AutoText = new(nameof(AutoText), 212);
    public static readonly AutoDetailsComponentType AutoMoneyRounded = new(nameof(AutoMoneyRounded), 213);
    public static readonly AutoDetailsComponentType AutoCNPJFormatted = new(nameof(AutoCNPJFormatted), 214);
    public static readonly AutoDetailsComponentType AutoStatus = new(nameof(AutoStatus), 215);
    public static readonly AutoDetailsComponentType AutoTextArea = new(nameof(AutoTextArea), 216);
    public static readonly AutoDetailsComponentType AutoTextStatus = new(nameof(AutoTextStatus), 217);
    
    public static readonly AutoDetailsComponentType AutoTextTable = new(nameof(AutoTextTable), 218);
    
    /*public static readonly AutoDetailsComponentType AutoDateTable = new(nameof(AutoDateTable), 219);*/
    public static readonly AutoDetailsComponentType AutoWallet = new(nameof(AutoWallet), 220);
    public static readonly AutoDetailsComponentType AutoWalletTable = new(nameof(AutoWalletTable), 221);
    public static readonly AutoDetailsComponentType AutoAddress = new(nameof(AutoAddress), 222);
    public static readonly AutoDetailsComponentType AutoImageRectangleDetails = new(nameof(AutoImageRectangleDetails), 223);
    public static readonly AutoDetailsComponentType AutoMoneyRoundedTable = new(nameof(AutoMoneyRoundedTable), 224);
    public static readonly AutoDetailsComponentType AutoLastCreditCardNumbers = new(nameof(AutoLastCreditCardNumbers), 225);
    public static readonly AutoDetailsComponentType AutoUserGroupName = new(nameof(AutoUserGroupName), 226);
    public static readonly AutoDetailsComponentType AutoDecimal = new(nameof(AutoDecimal), 227);
    public static readonly AutoDetailsComponentType AutoTextLink = new(nameof(AutoTextLink), 228);
    
    
    public static readonly AutoDetailsComponentType AutoFileDetails = new(nameof(AutoFileDetails), 229);
    public static readonly AutoDetailsComponentType AutoSectionText = new(nameof(AutoSectionText), 230);
    public static readonly AutoDetailsComponentType AutoNumeric = new(nameof(AutoNumeric), 231);
    public static readonly AutoDetailsComponentType AutoGuidListCount = new(nameof(AutoGuidListCount), 232);
    public static readonly AutoDetailsComponentType AutoDataGrid = new(nameof(AutoDataGrid), 233);
    public static readonly AutoDetailsComponentType AutoTextEnum = new(nameof(AutoTextEnum), 234);
    public static readonly AutoDetailsComponentType AutoTitle = new(nameof(AutoTitle), 235);
    
    public static readonly AutoDetailsComponentType AutoMPPT = new(nameof(AutoMPPT), 236);
    public static readonly AutoDetailsComponentType AutoLiporProjectStatusEnum = new(nameof(AutoLiporProjectStatusEnum), 237);
}