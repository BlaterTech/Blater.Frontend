namespace Blater.Frontend.Client.Contracts.Tenant;

public class TenantTheme
{
    /// <summary>
    ///     Url to the logo
    /// </summary>
    public string? NavMenuLogo { get; set; }

    public string? NavMenuIconLogo { get; set; }
    public int NavMenuLogoWidth { get; set; }
    public int NavMenuLogoIconWidth { get; set; }

    public bool ContainsIcon { get; set; }
    public bool OnlyIcon { get; set; }

    /// <summary>
    ///     Url to the favicon
    /// </summary>
    public string? Favicon { get; set; }

    public string LoginLogo { get; set; } = null!;
    public string LoginBackgroundImage { get; set; } = null!;
    public bool IsDarkMode { get; set; }

    public TenantPaletteDark? PaletteDark { get; set; }
    public TenantPaletteLight? PaletteLight { get; set; }

    public TenantTypography? Typography { get; set; } = new();
}