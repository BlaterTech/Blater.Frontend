using Blater.Enumerations;
using Blater.Models.Bases;
using Blater.Utilities;

namespace Blater.Frontend.Client.Models;

public class TenantData : BaseDataModel
{
    public Guid TenantId { get; set; } = SequentialGuidGenerator.NewGuid();
    
    public required string Name { get; set; }
    public required string Domain { get; set; }
    public required string SubDomain { get; set; }
    
    /// <summary>
    ///     Used to set the database name and subdomain
    /// </summary>
    public required string ShortName { get; set; }
    
    /// <summary>
    ///     The platform of the Tenant for example Medpoints,Juriself etc
    /// </summary>
    public BlaterProjects Project { get; set; } = BlaterProjects.None;
    
    /// <summary>
    ///     If the module has B2B functionality
    /// </summary>
    public bool MultipleTenants { get; set; }
    
    /// <summary>
    ///     Set to true if the module has OAuth functionality and the user can create an account with OAuth
    /// </summary>
    public bool CanCreateAccountOAuth { get; set; }
    
    #region Theme
    
    /// <summary>
    ///     Url to the logo
    /// </summary>
    public string? NavMenuLogo { get; set; }
    
    /// <summary>
    ///     Url to the favicon
    /// </summary>
    public string? Favicon { get; set; }
    
    public required string LoginLogo { get; set; }
    public required string LoginBackgroundImage { get; set; }
    public required string ThemePrimary { get; set; }
    public required string ThemeSecondary { get; set; }
    public required string ThemeTertiary { get; set; }
    public required string ThemePrimaryDarken { get; set; }
    public required string ThemePrimaryLighten { get; set; }
    public required string ThemeSecondaryDarken { get; set; }
    public required string ThemeSecondaryLighten { get; set; }
    public required string ThemeSecondaryContrastText { get; set; }
    public required string ThemeTertiaryDarken { get; set; }
    public required string ThemeTertiaryLighten { get; set; }
    public required string ThemeDrawerBackground { get; set; }
    public required string ThemeDrawerText { get; set; }
    public required string ThemeDrawerIcon { get; set; }
    public required string ThemeAppbarBackground { get; set; }
    public required string ThemeAppbarText { get; set; }
    public required string ThemeSurface { get; set; }
    public required string ThemeBackground { get; set; }
    
    #endregion
}