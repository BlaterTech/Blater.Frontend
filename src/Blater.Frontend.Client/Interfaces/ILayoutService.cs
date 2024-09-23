using Blater.Frontend.Client.Enumerations;
using MudBlazor;

namespace Blater.Frontend.Client.Interfaces;

public interface ILayoutService
{
    bool IsRTL { get; }
    DarkLightMode CurrentDarkLightMode { get; }
    bool IsDarkMode { get; }
    bool ObserveSystemThemeChange { get; }
    MudTheme CurrentTheme { get; }
    void SetDarkMode(bool value);
    Task ApplyUserPreferences(bool isDarkModeDefaultTheme);
    Task OnSystemPreferenceChanged(bool newValue);
    event EventHandler? MajorUpdateOccurred;
    Task CycleDarkLightModeAsync();
    Task ToggleRightToLeft();
    void SetBaseTheme(MudTheme theme);
}