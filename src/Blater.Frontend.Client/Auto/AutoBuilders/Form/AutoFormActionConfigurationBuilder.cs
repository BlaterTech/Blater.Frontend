using Blater.Frontend.Client.Auto.AutoModels.Form;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormActionConfigurationBuilder(FormActionConfiguration configuration)
{
    #region Divider

    public AutoFormActionConfigurationBuilder DividerType(DividerType value)
    {
        configuration.DividerType = value;
        return this;
    }

    public AutoFormActionConfigurationBuilder DividerExtraClass(string value)
    {
        configuration.DividerExtraClass = value;
        return this;
    }

    #endregion

    #region Actions

    public AutoFormActionConfigurationBuilder ActionExtraClass(string value)
    {
        configuration.ActionExtraClass = value;
        return this;
    }

    #region CancelButton

    public AutoFormActionConfigurationBuilder VariantCancelButton(Variant value)
    {
        configuration.VariantCancelButton = value;
        return this;
    }

    public AutoFormActionConfigurationBuilder ColorCancelButton(Color value)
    {
        configuration.ColorCancelButton = value;
        return this;
    }
    
    public AutoFormActionConfigurationBuilder CancelButtonExtraClass(string value)
    {
        configuration.CancelButtonExtraClass = value;
        return this;
    }

    #endregion

    #region Create/Edit Button

    public AutoFormActionConfigurationBuilder VariantCreateEditButton(Variant value)
    {
        configuration.VariantCreateEditButton = value;
        return this;
    }

    public AutoFormActionConfigurationBuilder ColorCreateEditButton(Color value)
    {
        configuration.ColorCreateEditButton = value;
        return this;
    }
    
    public AutoFormActionConfigurationBuilder TypeCreateEditButton(ButtonType value)
    {
        configuration.TypeCreateEditButton = value;
        return this;
    }
    
    public AutoFormActionConfigurationBuilder CreateEditButtonExtraClass(string value)
    {
        configuration.CreateEditButtonExtraClass = value;
        return this;
    }

    #endregion

    #endregion
}