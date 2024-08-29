using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Enumerations;

public class AutoFormComponentInputType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoFormComponentInputType AutoPasswordComponentInput = new(nameof(AutoPasswordComponentInput), 101);
    
    /// <summary>
    ///     File input for images
    /// </summary>
    public static readonly AutoFormComponentInputType AutoImageComponentInput = new(nameof(AutoImageComponentInput), 102);
    
    public static readonly AutoFormComponentInputType AutoImageCircleComponentInput = new(nameof(AutoImageCircleComponentInput), 201);
    
    public static readonly AutoFormComponentInputType AutoGroupComponentInput = new(nameof(AutoGroupComponentInput), 103);
    public static readonly AutoFormComponentInputType AutoRouteComponentInput = new(nameof(AutoRouteComponentInput), 104);
    public static readonly AutoFormComponentInputType AutoCnpjComponentInput = new(nameof(AutoCnpjComponentInput), 105);
    public static readonly AutoFormComponentInputType AutoCheckBoxComponentInput = new(nameof(AutoCheckBoxComponentInput), 106);
    public static readonly AutoFormComponentInputType AutoTextComponentInput = new(nameof(AutoTextComponentInput), 107);
    public static readonly AutoFormComponentInputType AutoTextAreaComponentInput = new(nameof(AutoTextAreaComponentInput), 108);
    public static readonly AutoFormComponentInputType AutoDateTimeComponentInput = new(nameof(AutoDateTimeComponentInput), 109);
    public static readonly AutoFormComponentInputType AutoToggleComponentInput = new(nameof(AutoToggleComponentInput), 110);
    public static readonly AutoFormComponentInputType AutoMoneyComponentInput = new(nameof(AutoMoneyComponentInput), 111);
    public static readonly AutoFormComponentInputType AutoNumericComponentInput = new(nameof(AutoNumericComponentInput), 112);
    public static readonly AutoFormComponentInputType AutoNumericTextComponentInput = new(nameof(AutoNumericTextComponentInput), 113);
    
    public static readonly AutoFormComponentInputType AutoSelectComponentInput = new(nameof(AutoSelectComponentInput), 115);
    public static readonly AutoFormComponentInputType AutoStateComponentInput = new(nameof(AutoStateComponentInput), 116);
    public static readonly AutoFormComponentInputType AutoSearchGuidComponentInput = new(nameof(AutoSearchGuidComponentInput), 117);
    public static readonly AutoFormComponentInputType AutoSearchBadgeComponentInput = new(nameof(AutoSearchBadgeComponentInput), 118);
    public static readonly AutoFormComponentInputType AutoAddressComponentInput = new(nameof(AutoAddressComponentInput), 119);
    public static readonly AutoFormComponentInputType AutoSelectPermissionsComponentInput = new(nameof(AutoSelectPermissionsComponentInput), 120);
    public static readonly AutoFormComponentInputType AutoSelectUserTypeComponentInput = new(nameof(AutoSelectUserTypeComponentInput), 121);
    public static readonly AutoFormComponentInputType AutoDecimalComponentInput = new(nameof(AutoDecimalComponentInput), 122);
    public static readonly AutoFormComponentInputType AutoSwitchComponentInput = new(nameof(AutoSwitchComponentInput), 123);
    public static readonly AutoFormComponentInputType AutoEnableSwitchComponentInput = new(nameof(AutoEnableSwitchComponentInput), 124);
    
    /// <summary>
    ///     File input in general
    /// </summary>
    public static readonly AutoFormComponentInputType AutoFileComponentInput = new(nameof(AutoFileComponentInput), 125);
    
    public static readonly AutoFormComponentInputType AutoCpfComponentInput = new(nameof(AutoCpfComponentInput), 126);
    public static readonly AutoFormComponentInputType AutoEditPasswordComponentInput = new(nameof(AutoEditPasswordComponentInput), 127);
    public static readonly AutoFormComponentInputType AutoSelectNoTranslationComponentInput = new(nameof(AutoSelectNoTranslationComponentInput), 128);
    public static readonly AutoFormComponentInputType AutoMpptComponentInput = new(nameof(AutoMpptComponentInput), 129);
    
    public static readonly AutoFormComponentInputType AutoTitle = new(nameof(AutoTitle), 199);
    
    public override bool HasValueChanged { get; set; } = true;
}