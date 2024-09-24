using Blater.Extensions;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models.Tenant;
using Microsoft.Extensions.Options;
using MudBlazor;

namespace Blater.Frontend.Client.Services;

public class TenantThemeConfigurationService(IOptions<TenantData> tenantData, ILayoutService layoutService) : ITenantThemeConfigurationService
{
    private readonly TenantTheme _tenantTheme = tenantData.Value.TenantTheme;

    public MudTheme GetMudTheme()
    {
        var themePaletteDark = _tenantTheme.PaletteDark;
        var themePaletteLight = _tenantTheme.PaletteLight;
        var themeTypography = _tenantTheme.Typography;
        var defaultTheme = layoutService.CurrentTheme;
        var theme = new MudTheme();

        if (themePaletteDark != null)
        {
            theme.PaletteDark = new PaletteDark
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
                
                Background = themePaletteDark.Background ?? defaultTheme.PaletteDark.Background,
                Surface = themePaletteDark.Surface ?? defaultTheme.PaletteDark.Surface,
                PrimaryContrastText = themePaletteDark.PrimaryContrastText ?? defaultTheme.PaletteDark.PrimaryContrastText,
            };
        }

        if (themePaletteLight != null)
        {
            theme.PaletteLight = new PaletteLight
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
                
                Background = themePaletteLight.Background ?? defaultTheme.PaletteLight.Background,
                Surface = themePaletteLight.Surface ?? defaultTheme.PaletteLight.Surface,
                PrimaryContrastText = themePaletteLight.PrimaryContrastText ?? defaultTheme.PaletteLight.PrimaryContrastText,
            };
        }

        if (themeTypography is {Button: not null})
        {
            theme.Typography = new Typography
            {
                Button = new Button
                {
                    FontWeight = themeTypography.Button.FontWeight,
                    FontSize = themeTypography.Button.FontSize
                }
            };
        }
        
        return theme;
    }
}