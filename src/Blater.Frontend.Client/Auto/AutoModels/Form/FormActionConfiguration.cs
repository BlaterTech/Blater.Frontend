using System.ComponentModel;
using System.Runtime.CompilerServices;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class AutoFormActionConfiguration : INotifyPropertyChanged
{
    private Color _colorCancelButton;

    #region Divider

    public DividerType DividerType { get; set; } = DividerType.FullWidth;
    public string DividerExtraClass { get; set; } = string.Empty;

    #endregion

    #region Actions

    public string ActionExtraClass { get; set; } = string.Empty;

    #region CancelButton

    public Variant VariantCancelButton { get; set; } = Variant.Filled;

    public Color ColorCancelButton
    {
        get => _colorCancelButton;
        set
        {
            if (value == _colorCancelButton) return;
            _colorCancelButton = value;
            OnPropertyChanged();
        }
    }

    public string CancelButtonExtraClass { get; set; } = string.Empty;
    #endregion

    #region Create/Edit Button

    public Variant VariantCreateEditButton { get; set; } = Variant.Filled;
    public Color ColorCreateEditButton { get; set; } = Color.Primary;
    public ButtonType TypeCreateEditButton { get; set; } = ButtonType.Submit;
    public string CreateEditButtonExtraClass { get; set; } = string.Empty;

    #endregion


    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}