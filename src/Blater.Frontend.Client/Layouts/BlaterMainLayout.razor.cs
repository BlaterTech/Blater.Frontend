using Blater.Frontend.Client.Interfaces;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Blater.Frontend.Client.Layouts;

public partial class BlaterMainLayout : LayoutComponentBase
{
    [Inject]
    protected ILayoutService LayoutService { get; set; } = default!;

    [Inject]
    protected ITenantThemeConfigurationService ThemeConfiguration { get; set; } = default!;

    [Inject]
    protected INavigationService NavigationService { get; set; } = default!;

    [Inject]
    protected ILocalizationService LocalizationService { get; set; } = default!;

    private MudThemeProvider _mudThemeProvider = null!;

    protected override void OnInitialized()
    {
        LayoutService.MajorUpdateOccurred += LayoutServiceOnMajorUpdateOccured!;

        var tenantTheme = ThemeConfiguration.GetMudTheme();
        LayoutService.SetBaseTheme(tenantTheme);
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await ApplyUserPreferences();
            await _mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
            StateHasChanged();
        }
    }

    private async Task ApplyUserPreferences()
    {
        var defaultDarkMode = await _mudThemeProvider.GetSystemPreference();
        await LayoutService.ApplyUserPreferences(defaultDarkMode);
    }

    private async Task OnSystemPreferenceChanged(bool newValue)
    {
        await LayoutService.OnSystemPreferenceChanged(newValue);
    }

    public void Dispose()
    {
        LayoutService.MajorUpdateOccurred -= LayoutServiceOnMajorUpdateOccured!;
    }

    private void LayoutServiceOnMajorUpdateOccured(object sender, EventArgs e) => StateHasChanged();
}