namespace Blater.Frontend.Client.Models.Tenant;

public class TenantTheme
{
    /// <summary>
    ///     Url to the logo
    /// </summary>
    public string? NavMenuLogo { get; set; }
    
    /// <summary>
    ///     Url to the favicon
    /// </summary>
    public string? Favicon { get; set; }
    
    public string LoginLogo { get; set; } = null!;
    public string LoginBackgroundImage { get; set; } = null!;
    
    public string Primary { get; set; } = null!;
    public string Secondary { get; set; } = null!;
    public string Tertiary { get; set; } = null!;
    public string PrimaryDarken { get; set; } = null!;
    public string PrimaryLighten { get; set; } = null!;
    public string SecondaryDarken { get; set; } = null!;
    public string SecondaryLighten { get; set; } = null!;
    public string SecondaryContrastText { get; set; } = null!;
    public string TertiaryDarken { get; set; } = null!;
    public string TertiaryLighten { get; set; } = null!;
    public string DrawerBackground { get; set; } = null!;
    public string DrawerText { get; set; } = null!;
    public string DrawerIcon { get; set; } = null!;
    public string AppbarBackground { get; set; } = null!;
    public string AppbarText { get; set; } = null!;
    public string Surface { get; set; } = null!;
    public string Background { get; set; } = null!;
}