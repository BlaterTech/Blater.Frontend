using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Enumerations;

public class AutoTableComponentType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoTableComponentType ImageRectangle = new(nameof(ImageRectangle), 201);
    public static readonly AutoTableComponentType ImageRounded = new(nameof(ImageRounded), 202);
    public static readonly AutoTableComponentType ImageCircle = new(nameof(ImageCircle), 203);
    public static readonly AutoTableComponentType AutoBadge = new(nameof(AutoBadge), 205);
    public static readonly AutoTableComponentType AutoShortId = new(nameof(AutoShortId), 206);
    public static readonly AutoTableComponentType AutoMoney = new(nameof(AutoMoney), 207);
    public static readonly AutoTableComponentType AutoImageCircle = new(nameof(AutoImageCircle), 208);
    public static readonly AutoTableComponentType AutoImageRectangle = new(nameof(AutoImageRectangle), 209);
    public static readonly AutoTableComponentType AutoImageRounded = new(nameof(AutoImageRounded), 210);
    public static readonly AutoTableComponentType AutoDate = new(nameof(AutoDate), 211);
    public static readonly AutoTableComponentType AutoText = new(nameof(AutoText), 212);
    public static readonly AutoTableComponentType AutoMoneyRounded = new(nameof(AutoMoneyRounded), 213);
    public static readonly AutoTableComponentType AutoCNPJFormatted = new(nameof(AutoCNPJFormatted), 214);
    public static readonly AutoTableComponentType AutoStatus = new(nameof(AutoStatus), 215);
    public static readonly AutoTableComponentType AutoTextArea = new(nameof(AutoTextArea), 216);
    public static readonly AutoTableComponentType AutoTextStatus = new(nameof(AutoTextStatus), 217);
    
    public static readonly AutoTableComponentType AutoTextTable = new(nameof(AutoTextTable), 218);
    
    /*public static readonly AutoTableComponentType AutoDateTable = new(nameof(AutoDateTable), 219);*/
    public static readonly AutoTableComponentType AutoWallet = new(nameof(AutoWallet), 220);
    public static readonly AutoTableComponentType AutoWalletTable = new(nameof(AutoWalletTable), 221);
    public static readonly AutoTableComponentType AutoAddress = new(nameof(AutoAddress), 222);
    public static readonly AutoTableComponentType AutoImageRectangleDetails = new(nameof(AutoImageRectangleDetails), 223);
    public static readonly AutoTableComponentType AutoMoneyRoundedTable = new(nameof(AutoMoneyRoundedTable), 224);
    public static readonly AutoTableComponentType AutoLastCreditCardNumbers = new(nameof(AutoLastCreditCardNumbers), 225);
    public static readonly AutoTableComponentType AutoUserGroupName = new(nameof(AutoUserGroupName), 226);
    public static readonly AutoTableComponentType AutoDecimal = new(nameof(AutoDecimal), 227);
    public static readonly AutoTableComponentType AutoTextLink = new(nameof(AutoTextLink), 228);
    
    
    public static readonly AutoTableComponentType AutoFileDetails = new(nameof(AutoFileDetails), 229);
    public static readonly AutoTableComponentType AutoSectionText = new(nameof(AutoSectionText), 230);
    public static readonly AutoTableComponentType AutoNumeric = new(nameof(AutoNumeric), 231);
    public static readonly AutoTableComponentType AutoGuidListCount = new(nameof(AutoGuidListCount), 232);
    public static readonly AutoTableComponentType AutoDataGrid = new(nameof(AutoDataGrid), 233);
    public static readonly AutoTableComponentType AutoTextEnum = new(nameof(AutoTextEnum), 234);
    public static readonly AutoTableComponentType AutoTitle = new(nameof(AutoTitle), 235);
    
    public static readonly AutoTableComponentType AutoMPPT = new(nameof(AutoMPPT), 236);
    public static readonly AutoTableComponentType AutoLiporProjectStatusEnum = new(nameof(AutoLiporProjectStatusEnum), 237);
}