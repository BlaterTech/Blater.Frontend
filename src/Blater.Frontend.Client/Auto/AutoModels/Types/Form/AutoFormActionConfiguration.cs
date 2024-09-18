using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormActionConfiguration<TModel>
{
    #region Divider

    public DividerType DividerType { get; } = DividerType.FullWidth;
    public string DividerExtraClass { get; } = string.Empty;

    #endregion

    #region Actions

    public string ActionExtraClass { get; } = string.Empty;

    #region CancelButton

    public Variant VariantCancelButton { get; } = Variant.Filled;

    public Color ColorCancelButton { get; } = Color.Secondary;

    public string CancelButtonExtraClass { get; } = string.Empty;
    #endregion

    #region Create/Edit Button

    public Variant VariantCreateEditButton { get; } = Variant.Filled;
    public Color ColorCreateEditButton { get; } = Color.Primary;
    public ButtonType TypeCreateEditButton { get; } = ButtonType.Submit;
    public string CreateEditButtonExtraClass { get; } = string.Empty;

    #endregion


    #endregion
}