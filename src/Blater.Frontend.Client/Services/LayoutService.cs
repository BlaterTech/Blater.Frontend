using Blater.Frontend.Client.Contracts;
using Blater.Frontend.Client.Enumerations;
using Blater.Frontend.Client.Interfaces;
using MudBlazor;

namespace Blater.Frontend.Client.Services;

public class LayoutService(IUserPreferencesService userPreferencesService) : ILayoutService
{
    private UserPreferences? _userPreferences;
    private bool _systemPreferences;

    public bool IsRTL { get; private set; }

    public DarkLightMode CurrentDarkLightMode { get; private set; } = DarkLightMode.System;

    public bool IsDarkMode { get; private set; }

    public bool ObserveSystemThemeChange { get; private set; }

    public MudTheme CurrentTheme { get; private set; } = new();
    
    public void SetDarkMode(bool value)
    {
        IsDarkMode = value;
    }

    public async Task ApplyUserPreferences(bool isDarkModeDefaultTheme)
    {
        _systemPreferences = isDarkModeDefaultTheme;

        _userPreferences = await userPreferencesService.LoadUserPreferences();

        if (_userPreferences != null)
        {
            CurrentDarkLightMode = _userPreferences.DarkLightTheme;
            IsDarkMode = CurrentDarkLightMode switch
            {
                DarkLightMode.Dark => true,
                DarkLightMode.Light => false,
                DarkLightMode.System => isDarkModeDefaultTheme,
                _ => IsDarkMode
            };

            IsRTL = _userPreferences.RightToLeft;
        }
        else
        {
            IsDarkMode = isDarkModeDefaultTheme;
            _userPreferences = new UserPreferences { DarkLightTheme = DarkLightMode.System };
            await userPreferencesService.SaveUserPreferences(_userPreferences);
        }
    }

    public Task OnSystemPreferenceChanged(bool newValue)
    {
        _systemPreferences = newValue;

        if (CurrentDarkLightMode == DarkLightMode.System)
        {
            IsDarkMode = newValue;
            OnMajorUpdateOccurred();
        }

        return Task.CompletedTask;
    }

    public event EventHandler? MajorUpdateOccurred;

    private void OnMajorUpdateOccurred() => MajorUpdateOccurred?.Invoke(this, EventArgs.Empty);

    public async Task CycleDarkLightModeAsync()
    {
        switch (CurrentDarkLightMode)
        {
            // Change to Light
            case DarkLightMode.System:
                CurrentDarkLightMode = DarkLightMode.Light;
                ObserveSystemThemeChange = false;
                IsDarkMode = false;
                break;
            // Change to Dark
            case DarkLightMode.Light:
                CurrentDarkLightMode = DarkLightMode.Dark;
                ObserveSystemThemeChange = false;
                IsDarkMode = true;
                break;
            // Change to System
            case DarkLightMode.Dark:
                CurrentDarkLightMode = DarkLightMode.System;
                ObserveSystemThemeChange = true;
                IsDarkMode = _systemPreferences;
                break;
        }

        _userPreferences!.DarkLightTheme = CurrentDarkLightMode;
        await userPreferencesService.SaveUserPreferences(_userPreferences);
        OnMajorUpdateOccurred();
    }

    public async Task ToggleRightToLeft()
    {
        IsRTL = !IsRTL;
        _userPreferences!.RightToLeft = IsRTL;
        await userPreferencesService.SaveUserPreferences(_userPreferences);
        OnMajorUpdateOccurred();
    }

    public void SetBaseTheme(MudTheme theme)
    {
        CurrentTheme = theme;
        OnMajorUpdateOccurred();
    }
}