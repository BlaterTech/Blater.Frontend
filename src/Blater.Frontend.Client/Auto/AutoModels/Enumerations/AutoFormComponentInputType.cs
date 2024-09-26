using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Enumerations;

public class AutoFormComponentInputType(string name, int value) : BaseAutoComponentTypeEnumeration(name, value)
{
    public static readonly AutoFormComponentInputType AutoFormPasswordComponentInput = new(nameof(AutoFormPasswordComponentInput), 101);
    
    /// <summary>
    ///     File input for images
    /// </summary>
    public static readonly AutoFormComponentInputType AutoFormImageComponentInput = new(nameof(AutoFormImageComponentInput), 102);
    
    public static readonly AutoFormComponentInputType AutoFormImageCircleComponentInput = new(nameof(AutoFormImageCircleComponentInput), 201);
    
    public static readonly AutoFormComponentInputType AutoFormGroupComponentInput = new(nameof(AutoFormGroupComponentInput), 103);
    public static readonly AutoFormComponentInputType AutoFormRouteComponentInput = new(nameof(AutoFormRouteComponentInput), 104);
    public static readonly AutoFormComponentInputType AutoFormCnpjComponentInput = new(nameof(AutoFormCnpjComponentInput), 105);
    public static readonly AutoFormComponentInputType AutoFormCheckBoxComponentInput = new(nameof(AutoFormCheckBoxComponentInput), 106);
    public static readonly AutoFormComponentInputType AutoFormTextComponentInput = new(nameof(AutoFormTextComponentInput), 107);
    public static readonly AutoFormComponentInputType AutoFormTextAreaComponentInput = new(nameof(AutoFormTextAreaComponentInput), 108);
    public static readonly AutoFormComponentInputType AutoFormDateTimeComponentInput = new(nameof(AutoFormDateTimeComponentInput), 109);
    public static readonly AutoFormComponentInputType AutoFormToggleComponentInput = new(nameof(AutoFormToggleComponentInput), 110);
    public static readonly AutoFormComponentInputType AutoFormMoneyComponentInput = new(nameof(AutoFormMoneyComponentInput), 111);
    public static readonly AutoFormComponentInputType AutoFormNumericComponentInput = new(nameof(AutoFormNumericComponentInput), 112);
    public static readonly AutoFormComponentInputType AutoFormNumericTextComponentInput = new(nameof(AutoFormNumericTextComponentInput), 113);
    
    public static readonly AutoFormComponentInputType AutoFormSelectComponentInput = new(nameof(AutoFormSelectComponentInput), 115);
    public static readonly AutoFormComponentInputType AutoFormStateComponentInput = new(nameof(AutoFormStateComponentInput), 116);
    public static readonly AutoFormComponentInputType AutoFormSearchGuidComponentInput = new(nameof(AutoFormSearchGuidComponentInput), 117);
    public static readonly AutoFormComponentInputType AutoFormSearchBadgeComponentInput = new(nameof(AutoFormSearchBadgeComponentInput), 118);
    public static readonly AutoFormComponentInputType AutoFormAddressComponentInput = new(nameof(AutoFormAddressComponentInput), 119);
    public static readonly AutoFormComponentInputType AutoFormSelectPermissionsComponentInput = new(nameof(AutoFormSelectPermissionsComponentInput), 120);
    public static readonly AutoFormComponentInputType AutoFormSelectUserTypeComponentInput = new(nameof(AutoFormSelectUserTypeComponentInput), 121);
    public static readonly AutoFormComponentInputType AutoFormDecimalComponentInput = new(nameof(AutoFormDecimalComponentInput), 122);
    public static readonly AutoFormComponentInputType AutoFormSwitchComponentInput = new(nameof(AutoFormSwitchComponentInput), 123);
    public static readonly AutoFormComponentInputType AutoFormEnableSwitchComponentInput = new(nameof(AutoFormEnableSwitchComponentInput), 124);
    
    /// <summary>
    ///     File input in general
    /// </summary>
    public static readonly AutoFormComponentInputType AutoFormFileComponentInput = new(nameof(AutoFormFileComponentInput), 125);
    
    public static readonly AutoFormComponentInputType AutoFormCpfComponentInput = new(nameof(AutoFormCpfComponentInput), 126);
    public static readonly AutoFormComponentInputType AutoFormEditPasswordComponentInput = new(nameof(AutoFormEditPasswordComponentInput), 127);
    public static readonly AutoFormComponentInputType AutoFormSelectNoTranslationComponentInput = new(nameof(AutoFormSelectNoTranslationComponentInput), 128);
    public static readonly AutoFormComponentInputType AutoFormMpptComponentInput = new(nameof(AutoFormMpptComponentInput), 129);
    
    public static readonly AutoFormComponentInputType AutoFormTitle = new(nameof(AutoFormTitle), 199);
    
    public override bool HasValueChanged => true;
}