using Azure;
using Azure.Data.Tables;
using Blater.Enumerations;

namespace Blater.Frontend.Models;

public class TenantData : ITableEntity
{
    public Guid TenantId => Guid.Parse(RowKey);
    
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
    
    public string PartitionKey { get; set; } = nameof(TenantData);
    public string RowKey { get; set; } = Guid.NewGuid().ToString();
    
    public DateTimeOffset? Timestamp { get; set; } = DateTimeOffset.UtcNow;
    public ETag ETag { get; set; }
    
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
    
    #endregion
}