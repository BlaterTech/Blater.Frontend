using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Enumerations;

public class AutoComponentInputType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoComponentInputType AutoPasswordComponentInput = new(nameof(AutoPasswordComponentInput), 101);
    
    /// <summary>
    ///     File input for images
    /// </summary>
    public static readonly AutoComponentInputType AutoImageComponentInput = new(nameof(AutoImageComponentInput), 102);
    
    public static readonly AutoComponentInputType AutoImageCircleComponentInput = new(nameof(AutoImageCircleComponentInput), 201);
    
    public static readonly AutoComponentInputType AutoGroupComponentInput = new(nameof(AutoGroupComponentInput), 103);
    public static readonly AutoComponentInputType AutoRouteComponentInput = new(nameof(AutoRouteComponentInput), 104);
    public static readonly AutoComponentInputType AutoCnpjComponentInput = new(nameof(AutoCnpjComponentInput), 105);
    public static readonly AutoComponentInputType AutoCheckBoxComponentInput = new(nameof(AutoCheckBoxComponentInput), 106);
    public static readonly AutoComponentInputType AutoTextComponentInput = new(nameof(AutoTextComponentInput), 107);
    public static readonly AutoComponentInputType AutoTextAreaComponentInput = new(nameof(AutoTextAreaComponentInput), 108);
    public static readonly AutoComponentInputType AutoDateTimeComponentInput = new(nameof(AutoDateTimeComponentInput), 109);
    public static readonly AutoComponentInputType AutoToggleComponentInput = new(nameof(AutoToggleComponentInput), 110);
    public static readonly AutoComponentInputType AutoMoneyComponentInput = new(nameof(AutoMoneyComponentInput), 111);
    public static readonly AutoComponentInputType AutoNumericComponentInput = new(nameof(AutoNumericComponentInput), 112);
    public static readonly AutoComponentInputType AutoNumericTextComponentInput = new(nameof(AutoNumericTextComponentInput), 113);
    
    public static readonly AutoComponentInputType AutoSelectComponentInput = new(nameof(AutoSelectComponentInput), 115);
    public static readonly AutoComponentInputType AutoStateComponentInput = new(nameof(AutoStateComponentInput), 116);
    public static readonly AutoComponentInputType AutoSearchGuidComponentInput = new(nameof(AutoSearchGuidComponentInput), 117);
    public static readonly AutoComponentInputType AutoSearchBadgeComponentInput = new(nameof(AutoSearchBadgeComponentInput), 118);
    public static readonly AutoComponentInputType AutoAddressComponentInput = new(nameof(AutoAddressComponentInput), 119);
    public static readonly AutoComponentInputType AutoSelectPermissionsComponentInput = new(nameof(AutoSelectPermissionsComponentInput), 120);
    public static readonly AutoComponentInputType AutoSelectUserTypeComponentInput = new(nameof(AutoSelectUserTypeComponentInput), 121);
    public static readonly AutoComponentInputType AutoDecimalComponentInput = new(nameof(AutoDecimalComponentInput), 122);
    public static readonly AutoComponentInputType AutoSwitchComponentInput = new(nameof(AutoSwitchComponentInput), 123);
    public static readonly AutoComponentInputType AutoEnableSwitchComponentInput = new(nameof(AutoEnableSwitchComponentInput), 124);
    
    /// <summary>
    ///     File input in general
    /// </summary>
    public static readonly AutoComponentInputType AutoFileComponentInput = new(nameof(AutoFileComponentInput), 125);
    
    public static readonly AutoComponentInputType AutoCpfComponentInput = new(nameof(AutoCpfComponentInput), 126);
    public static readonly AutoComponentInputType AutoEditPasswordComponentInput = new(nameof(AutoEditPasswordComponentInput), 127);
    public static readonly AutoComponentInputType AutoSelectNoTranslationComponentInput = new(nameof(AutoSelectNoTranslationComponentInput), 128);
    public static readonly AutoComponentInputType AutoMpptComponentInput = new(nameof(AutoMpptComponentInput), 129);
    
    public static readonly AutoComponentInputType AutoTitle = new(nameof(AutoTitle), 199);
    
    public override bool HasValueChanged { get; set; } = true;
}