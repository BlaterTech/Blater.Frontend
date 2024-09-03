using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoFormActionConfigurationBuilder
{
    AutoFormActionConfigurationBuilder DividerType(DividerType value);
    AutoFormActionConfigurationBuilder DividerExtraClass(string value);
    AutoFormActionConfigurationBuilder ActionExtraClass(string value);
    AutoFormActionConfigurationBuilder VariantCancelButton(Variant value);
    AutoFormActionConfigurationBuilder ColorCancelButton(Color value);
    AutoFormActionConfigurationBuilder CancelButtonExtraClass(string value);
    AutoFormActionConfigurationBuilder VariantCreateEditButton(Variant value);
    AutoFormActionConfigurationBuilder ColorCreateEditButton(Color value);
    AutoFormActionConfigurationBuilder TypeCreateEditButton(ButtonType value);
    AutoFormActionConfigurationBuilder CreateEditButtonExtraClass(string value);
}