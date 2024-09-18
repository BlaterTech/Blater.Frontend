using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormActionConfiguration
{
    #region Divider

    public DividerType DividerType { get; set; } = DividerType.FullWidth;
    public string DividerExtraClass { get; set; } = string.Empty;

    #endregion

    #region Actions

    public string ActionExtraClass { get; set; } = string.Empty;

    #region CancelButton

    public Variant VariantCancelButton { get; set; } = Variant.Filled;

    public Color ColorCancelButton { get; set; } = Color.Secondary;

    public string CancelButtonExtraClass { get; set; } = string.Empty;
    #endregion

    #region Create/Edit Button

    public Variant VariantCreateEditButton { get; set; } = Variant.Filled;
    public Color ColorCreateEditButton { get; set; } = Color.Primary;
    public ButtonType TypeCreateEditButton { get; set; } = ButtonType.Submit;
    public string CreateEditButtonExtraClass { get; set; } = string.Empty;

    #endregion


    #endregion
}