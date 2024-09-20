using MudBlazor;

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

    public bool IsDarkMode { get; set; }

    public MudTheme Theme { get; set; } = null!;
}