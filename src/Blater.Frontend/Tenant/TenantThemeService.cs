using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Interfaces;
using MudBlazor;

namespace Blater.Frontend.Tenant;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
public class TenantThemeService(ITenantService service) : ITenantThemeService, IDisposable
{
    public MudTheme GetMudTheme(Guid tenantId)
    {
        var tenantData = service.GetById(tenantId).GetAwaiter().GetResult();
        if (tenantData == null)
        {
            throw new InvalidOperationException($"TenantId '{tenantId}' not found");
        }
        
        var theme = new MudTheme
        {
            PaletteDark =
            {
                Primary = tenantData.ThemePrimary,
                PrimaryLighten = tenantData.ThemePrimaryLighten,
                PrimaryDarken = tenantData.ThemePrimaryDarken,
                Secondary = tenantData.ThemeSecondary,
                SecondaryLighten = tenantData.ThemeSecondaryLighten,
                SecondaryDarken = tenantData.ThemeSecondaryDarken,
                Tertiary = tenantData.ThemeTertiary,
                TertiaryLighten = tenantData.ThemeTertiaryLighten,
                TertiaryDarken = tenantData.ThemeTertiaryDarken,
                DrawerBackground = tenantData.ThemeDrawerBackground,
                DrawerText = tenantData.ThemeDrawerText,
                DrawerIcon = tenantData.ThemeDrawerText,
                AppbarBackground = tenantData.ThemeAppbarBackground,
                AppbarText = tenantData.ThemeAppbarText,
                Background = tenantData.ThemeBackground,
                Surface = tenantData.ThemeSurface,
            },
            PaletteLight = 
            {
                Primary = tenantData.ThemePrimary,
                PrimaryLighten = tenantData.ThemePrimaryLighten,
                PrimaryDarken = tenantData.ThemePrimaryDarken,
                Secondary = tenantData.ThemeSecondary,
                SecondaryLighten = tenantData.ThemeSecondaryLighten,
                SecondaryDarken = tenantData.ThemeSecondaryDarken,
                Tertiary = tenantData.ThemeTertiary,
                TertiaryLighten = tenantData.ThemeTertiaryLighten,
                TertiaryDarken = tenantData.ThemeTertiaryDarken,
                DrawerBackground = tenantData.ThemeDrawerBackground,
                DrawerText = tenantData.ThemeDrawerText,
                DrawerIcon = tenantData.ThemeDrawerText,
                AppbarBackground = tenantData.ThemeAppbarBackground,
                AppbarText = tenantData.ThemeAppbarText,
                Background = tenantData.ThemeBackground,
                Surface = tenantData.ThemeSurface,
                SecondaryContrastText = tenantData.ThemeSecondaryContrastText,
            },
            Typography = new Typography
            {
                Button = new Button
                {
                    FontWeight = 700,
                    FontSize = "0.85rem"
                }
            }
        };
        
        return theme;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}