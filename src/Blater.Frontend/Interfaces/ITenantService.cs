using Blater.Enumerations;
using Blater.Frontend.Models;

namespace Blater.Frontend.Interfaces;

public interface ITenantService
{
    /// <summary>
    ///     If project = none, then all tenants are returned
    /// </summary>
    /// GetCurrentTenant
    /// <param name="project"></param>
    /// <returns></returns>
    Task<IReadOnlyList<TenantData>?> GetAllTenants(BlaterProjects project);
    
    /// <summary>
    ///     Get the tenant data from the database using the short name
    /// </summary>
    /// <param name="shortName"></param>
    /// <returns></returns>
    Task<TenantData?> GetByShortName(string shortName);
    
    /// <summary>
    ///     Get the tenant data from the database using the tenant id
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Task<TenantData?> GetById(Guid tenantId);
    
    /// <summary>
    ///     Get the tenant data from the database using the string tenant id
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    Task<TenantData?> GetById(string tenantId);
    
    /// <summary>
    ///     Create the tenant data to the database
    /// </summary>
    /// <param name="tenantData"></param>
    /// <returns></returns>
    Task<TenantData?> Create(TenantData tenantData);
    
    /// <summary>
    ///     Update the tenant data from the database using the tenant id
    /// </summary>
    /// <param name="tenantData"></param>
    /// <returns></returns>
    Task<TenantData?> Update(TenantData tenantData);
    
    /// <summary>
    ///     Delete the tenant data from the database using the tenant id
    /// </summary>
    /// <param name="tenantData"></param>
    /// <returns></returns>
    Task<bool> Delete(TenantData tenantData);
    
    /// <summary>
    ///     Get the current tenant data from the datatable using the tenant id defined in the appsettings.json file
    /// </summary>
    /// <returns></returns>
    Task<TenantData?> GetCurrentTenant();
}