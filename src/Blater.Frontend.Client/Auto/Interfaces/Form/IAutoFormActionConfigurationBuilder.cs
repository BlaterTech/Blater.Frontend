using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces.Form;

public interface IAutoFormActionConfigurationBuilder
{
    IAutoFormActionConfigurationBuilder DividerType(DividerType value);
    IAutoFormActionConfigurationBuilder DividerExtraClass(string value);
    IAutoFormActionConfigurationBuilder ActionExtraClass(string value);
    IAutoFormActionConfigurationBuilder VariantCancelButton(Variant value);
    IAutoFormActionConfigurationBuilder ColorCancelButton(Color value);
    IAutoFormActionConfigurationBuilder CancelButtonExtraClass(string value);
    IAutoFormActionConfigurationBuilder VariantCreateEditButton(Variant value);
    IAutoFormActionConfigurationBuilder ColorCreateEditButton(Color value);
    IAutoFormActionConfigurationBuilder TypeCreateEditButton(ButtonType value);
    IAutoFormActionConfigurationBuilder CreateEditButtonExtraClass(string value);
}