using Blater.Enumerations;
using Blater.Models.Bases;
using Blater.Utilities;

namespace Blater.Frontend.Client.Models.Tenant;

public class TenantData : BaseDataModel
{
    public Guid TenantId { get; set; }
    
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
    
    public TenantTheme TenantTheme { get; set; } = new();
}