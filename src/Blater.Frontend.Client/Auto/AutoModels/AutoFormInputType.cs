namespace Blater.Frontend.Client.Auto.AutoModels;

public class AutoFormInputType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoFormInputType AutoPasswordInput = new(nameof(AutoPasswordInput), 101);
    
    /// <summary>
    ///     File input for images
    /// </summary>
    public static readonly AutoFormInputType AutoImageInput = new(nameof(AutoImageInput), 102);
    
    public static readonly AutoFormInputType AutoImageCircleInput = new(nameof(AutoImageCircleInput), 201);
    
    public static readonly AutoFormInputType AutoGroupInput = new(nameof(AutoGroupInput), 103);
    public static readonly AutoFormInputType AutoRouteInput = new(nameof(AutoRouteInput), 104);
    public static readonly AutoFormInputType AutoCNPJInput = new(nameof(AutoCNPJInput), 105);
    public static readonly AutoFormInputType AutoCheckBoxInput = new(nameof(AutoCheckBoxInput), 106);
    public static readonly AutoFormInputType AutoTextInput = new(nameof(AutoTextInput), 107);
    public static readonly AutoFormInputType AutoTextAreaInput = new(nameof(AutoTextAreaInput), 108);
    public static readonly AutoFormInputType AutoDateTimeInput = new(nameof(AutoDateTimeInput), 109);
    public static readonly AutoFormInputType AutoToggleInput = new(nameof(AutoToggleInput), 110);
    public static readonly AutoFormInputType AutoMoneyInput = new(nameof(AutoMoneyInput), 111);
    public static readonly AutoFormInputType AutoNumericInput = new(nameof(AutoNumericInput), 112);
    public static readonly AutoFormInputType AutoNumericTextInput = new(nameof(AutoNumericTextInput), 113);
    
    public static readonly AutoFormInputType AutoSelectInput = new(nameof(AutoSelectInput), 115);
    public static readonly AutoFormInputType AutoStateInput = new(nameof(AutoStateInput), 116);
    public static readonly AutoFormInputType AutoSearchGuidInput = new(nameof(AutoSearchGuidInput), 117);
    public static readonly AutoFormInputType AutoSearchBadgeInput = new(nameof(AutoSearchBadgeInput), 118);
    public static readonly AutoFormInputType AutoAddressInput = new(nameof(AutoAddressInput), 119);
    public static readonly AutoFormInputType AutoSelectPermissionsInput = new(nameof(AutoSelectPermissionsInput), 120);
    public static readonly AutoFormInputType AutoSelectUserTypeInput = new(nameof(AutoSelectUserTypeInput), 121);
    public static readonly AutoFormInputType AutoDecimalInput = new(nameof(AutoDecimalInput), 122);
    public static readonly AutoFormInputType AutoSwitchInput = new(nameof(AutoSwitchInput), 123);
    public static readonly AutoFormInputType AutoEnableSwitchInput = new(nameof(AutoEnableSwitchInput), 124);
    
    /// <summary>
    ///     File input in general
    /// </summary>
    public static readonly AutoFormInputType AutoFileInput = new(nameof(AutoFileInput), 125);
    
    public static readonly AutoFormInputType AutoCPFInput = new(nameof(AutoCPFInput), 126);
    public static readonly AutoFormInputType AutoEditPasswordInput = new(nameof(AutoEditPasswordInput), 127);
    public static readonly AutoFormInputType AutoSelectNoTranslationInput = new(nameof(AutoSelectNoTranslationInput), 128);
    public static readonly AutoFormInputType AutoMPPTInput = new(nameof(AutoMPPTInput), 129);
    
    public static readonly AutoFormInputType AutoTitle = new(nameof(AutoTitle), 199);
    
    public override bool HasValueChanged { get; set; } = true;
}