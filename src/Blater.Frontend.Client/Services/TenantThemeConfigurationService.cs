using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models.Tenant;
using Microsoft.Extensions.Options;
using MudBlazor;

namespace Blater.Frontend.Client.Services;

public class TenantThemeConfigurationService(IOptions<TenantData> options) : ITenantThemeConfigurationService
{
    private readonly TenantTheme _tenantTheme = options.Value.TenantTheme;

    public MudTheme DefaultTheme { get; set; } = new();
    public bool IsDarkMode => _tenantTheme.IsDarkMode;

    public MudTheme GetMudTheme()
    {
        var themePaletteDark = _tenantTheme.PaletteDark;
        var themePaletteLight = _tenantTheme.PaletteLight;
        var theme = new MudTheme
        {
            PaletteDark =
            {
                Primary = themePaletteDark.Primary ?? DefaultTheme.PaletteDark.Primary,
                PrimaryLighten = themePaletteDark.PrimaryLighten ?? DefaultTheme.PaletteDark.PrimaryLighten,
                PrimaryDarken = themePaletteDark.PrimaryDarken ?? DefaultTheme.PaletteDark.PrimaryDarken,
                
                Secondary = themePaletteDark.Secondary ?? DefaultTheme.PaletteDark.Secondary,
                SecondaryLighten = themePaletteDark.SecondaryLighten ?? DefaultTheme.PaletteDark.SecondaryLighten,
                SecondaryDarken = themePaletteDark.SecondaryDarken ?? DefaultTheme.PaletteDark.SecondaryDarken,
                
                Tertiary = themePaletteDark.Tertiary ?? DefaultTheme.PaletteDark.Tertiary,
                TertiaryLighten = themePaletteDark.TertiaryLighten ?? DefaultTheme.PaletteDark.TertiaryLighten,
                TertiaryDarken = themePaletteDark.TertiaryDarken ?? DefaultTheme.PaletteDark.TertiaryDarken,
                
                DrawerBackground = themePaletteDark.DrawerBackground ?? DefaultTheme.PaletteDark.DrawerBackground,
                DrawerText = themePaletteDark.DrawerText ?? DefaultTheme.PaletteDark.DrawerText,
                DrawerIcon = themePaletteDark.DrawerIcon ?? DefaultTheme.PaletteDark.DrawerIcon,
                
                AppbarBackground = themePaletteDark.AppbarBackground ?? DefaultTheme.PaletteDark.AppbarBackground,
                AppbarText = themePaletteDark.AppbarText ?? DefaultTheme.PaletteDark.AppbarText,
                
                Divider = themePaletteDark.Divider ?? DefaultTheme.PaletteDark.Divider,
                DividerLight = themePaletteDark.DividerLight ?? DefaultTheme.PaletteDark.DividerLight,
            },
            PaletteLight =
            {
                Primary = themePaletteLight.Primary ?? DefaultTheme.PaletteLight.Primary,
                PrimaryLighten = themePaletteLight.PrimaryLighten ?? DefaultTheme.PaletteLight.PrimaryLighten,
                PrimaryDarken = themePaletteLight.PrimaryDarken ?? DefaultTheme.PaletteLight.PrimaryDarken,
                
                Secondary = themePaletteLight.Secondary ?? DefaultTheme.PaletteLight.Secondary,
                SecondaryLighten = themePaletteLight.SecondaryLighten ?? DefaultTheme.PaletteLight.SecondaryLighten,
                SecondaryDarken = themePaletteLight.SecondaryDarken ?? DefaultTheme.PaletteLight.SecondaryDarken,
                
                Tertiary = themePaletteLight.Tertiary ?? DefaultTheme.PaletteLight.Tertiary,
                TertiaryLighten = themePaletteLight.TertiaryLighten ?? DefaultTheme.PaletteLight.TertiaryLighten,
                TertiaryDarken = themePaletteLight.TertiaryDarken ?? DefaultTheme.PaletteLight.TertiaryDarken,
                
                DrawerBackground = themePaletteLight.DrawerBackground ?? DefaultTheme.PaletteLight.DrawerBackground,
                DrawerText = themePaletteLight.DrawerText ?? DefaultTheme.PaletteLight.DrawerText,
                DrawerIcon = themePaletteLight.DrawerIcon ?? DefaultTheme.PaletteLight.DrawerIcon,
                
                AppbarBackground = themePaletteLight.AppbarBackground ?? DefaultTheme.PaletteLight.AppbarBackground,
                AppbarText = themePaletteLight.AppbarText ?? DefaultTheme.PaletteLight.AppbarText,
                
                Divider = themePaletteLight.Divider ?? DefaultTheme.PaletteLight.Divider,
                DividerLight = themePaletteLight.DividerLight ?? DefaultTheme.PaletteLight.DividerLight,
            }
        };

        return theme;
    }

    public TenantTheme GetTenantTheme()
        => _tenantTheme;
}