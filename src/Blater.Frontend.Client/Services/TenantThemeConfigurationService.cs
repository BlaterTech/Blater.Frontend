using Blater.Extensions;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models.Tenant;
using MudBlazor;

namespace Blater.Frontend.Client.Services;

public class TenantThemeConfigurationService(TenantData tenantData, ILayoutService layoutService) : ITenantThemeConfigurationService
{
    private readonly TenantTheme _tenantTheme = tenantData.TenantTheme;

    public MudTheme GetMudTheme()
    {
        Console.WriteLine($"TenantTheme => {_tenantTheme.ToJson()}");
        var themePaletteDark = _tenantTheme.PaletteDark;
        var themePaletteLight = _tenantTheme.PaletteLight;
        var defaultTheme = layoutService.CurrentTheme;
        var theme = new MudTheme
        {
            PaletteDark =
            {
                Primary = themePaletteDark.Primary ?? defaultTheme.PaletteDark.Primary,
                PrimaryLighten = themePaletteDark.PrimaryLighten ?? defaultTheme.PaletteDark.PrimaryLighten,
                PrimaryDarken = themePaletteDark.PrimaryDarken ?? defaultTheme.PaletteDark.PrimaryDarken,
                
                Secondary = themePaletteDark.Secondary ?? defaultTheme.PaletteDark.Secondary,
                SecondaryLighten = themePaletteDark.SecondaryLighten ?? defaultTheme.PaletteDark.SecondaryLighten,
                SecondaryDarken = themePaletteDark.SecondaryDarken ?? defaultTheme.PaletteDark.SecondaryDarken,
                
                Tertiary = themePaletteDark.Tertiary ?? defaultTheme.PaletteDark.Tertiary,
                TertiaryLighten = themePaletteDark.TertiaryLighten ?? defaultTheme.PaletteDark.TertiaryLighten,
                TertiaryDarken = themePaletteDark.TertiaryDarken ?? defaultTheme.PaletteDark.TertiaryDarken,
                
                DrawerBackground = themePaletteDark.DrawerBackground ?? defaultTheme.PaletteDark.DrawerBackground,
                DrawerText = themePaletteDark.DrawerText ?? defaultTheme.PaletteDark.DrawerText,
                DrawerIcon = themePaletteDark.DrawerIcon ?? defaultTheme.PaletteDark.DrawerIcon,
                
                AppbarBackground = themePaletteDark.AppbarBackground ?? defaultTheme.PaletteDark.AppbarBackground,
                AppbarText = themePaletteDark.AppbarText ?? defaultTheme.PaletteDark.AppbarText,
                
                Divider = themePaletteDark.Divider ?? defaultTheme.PaletteDark.Divider,
                DividerLight = themePaletteDark.DividerLight ?? defaultTheme.PaletteDark.DividerLight,
            },
            PaletteLight =
            {
                Primary = themePaletteLight.Primary ?? defaultTheme.PaletteLight.Primary,
                PrimaryLighten = themePaletteLight.PrimaryLighten ?? defaultTheme.PaletteLight.PrimaryLighten,
                PrimaryDarken = themePaletteLight.PrimaryDarken ?? defaultTheme.PaletteLight.PrimaryDarken,
                
                Secondary = themePaletteLight.Secondary ?? defaultTheme.PaletteLight.Secondary,
                SecondaryLighten = themePaletteLight.SecondaryLighten ?? defaultTheme.PaletteLight.SecondaryLighten,
                SecondaryDarken = themePaletteLight.SecondaryDarken ?? defaultTheme.PaletteLight.SecondaryDarken,
                
                Tertiary = themePaletteLight.Tertiary ?? defaultTheme.PaletteLight.Tertiary,
                TertiaryLighten = themePaletteLight.TertiaryLighten ?? defaultTheme.PaletteLight.TertiaryLighten,
                TertiaryDarken = themePaletteLight.TertiaryDarken ?? defaultTheme.PaletteLight.TertiaryDarken,
                
                DrawerBackground = themePaletteLight.DrawerBackground ?? defaultTheme.PaletteLight.DrawerBackground,
                DrawerText = themePaletteLight.DrawerText ?? defaultTheme.PaletteLight.DrawerText,
                DrawerIcon = themePaletteLight.DrawerIcon ?? defaultTheme.PaletteLight.DrawerIcon,
                
                AppbarBackground = themePaletteLight.AppbarBackground ?? defaultTheme.PaletteLight.AppbarBackground,
                AppbarText = themePaletteLight.AppbarText ?? defaultTheme.PaletteLight.AppbarText,
                
                Divider = themePaletteLight.Divider ?? defaultTheme.PaletteLight.Divider,
                DividerLight = themePaletteLight.DividerLight ?? defaultTheme.PaletteLight.DividerLight,
            }
        };

        return theme;
    }
}