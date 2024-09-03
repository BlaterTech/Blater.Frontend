using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Form;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormActionConfigurationBuilder(AutoFormActionConfiguration configuration) : IAutoFormActionConfigurationBuilder
{
    #region Divider

    public IAutoFormActionConfigurationBuilder DividerType(DividerType value)
    {
        configuration.DividerType = value;
        return this;
    }

    public IAutoFormActionConfigurationBuilder DividerExtraClass(string value)
    {
        configuration.DividerExtraClass = value;
        return this;
    }

    #endregion

    #region Actions

    public IAutoFormActionConfigurationBuilder ActionExtraClass(string value)
    {
        configuration.ActionExtraClass = value;
        return this;
    }

    #region CancelButton

    public IAutoFormActionConfigurationBuilder VariantCancelButton(Variant value)
    {
        configuration.VariantCancelButton = value;
        return this;
    }

    public IAutoFormActionConfigurationBuilder ColorCancelButton(Color value)
    {
        configuration.ColorCancelButton = value;
        return this;
    }
    
    public IAutoFormActionConfigurationBuilder CancelButtonExtraClass(string value)
    {
        configuration.CancelButtonExtraClass = value;
        return this;
    }

    #endregion

    #region Create/Edit Button

    public IAutoFormActionConfigurationBuilder VariantCreateEditButton(Variant value)
    {
        configuration.VariantCreateEditButton = value;
        return this;
    }

    public IAutoFormActionConfigurationBuilder ColorCreateEditButton(Color value)
    {
        configuration.ColorCreateEditButton = value;
        return this;
    }
    
    public IAutoFormActionConfigurationBuilder TypeCreateEditButton(ButtonType value)
    {
        configuration.TypeCreateEditButton = value;
        return this;
    }
    
    public IAutoFormActionConfigurationBuilder CreateEditButtonExtraClass(string value)
    {
        configuration.CreateEditButtonExtraClass = value;
        return this;
    }

    #endregion

    #endregion
}