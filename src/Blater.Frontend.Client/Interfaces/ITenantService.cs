using Blater.Enumerations;
using Blater.Frontend.Client.Contracts.Tenant;

namespace Blater.Frontend.Client.Interfaces;

public interface ITenantService
{
    Task<IReadOnlyList<TenantData>> Find();
    Task<IReadOnlyList<TenantData>> Find(BlaterProjects projects);

    Task<TenantData> GetById(Guid tenantId);
    Task<TenantData> GetById(string tenantId);
    Task<TenantData> GetByShortName(string shortName);
    
    Task<TenantData> Create(TenantData tenantData);
    Task<IReadOnlyList<TenantData>> Create(IEnumerable<TenantData> tenants);
    
    Task<TenantData> Update(TenantData tenantData);
    Task<IReadOnlyList<TenantData>> Update(IEnumerable<TenantData> tenants);
    
    Task<TenantData> Delete(TenantData tenantData);
    Task<TenantData> Delete(Guid tenantId);
    Task<TenantData> Delete(string tenantId);
    Task<TenantData> Delete(IEnumerable<TenantData> tenants);
    Task<TenantData> Delete(IEnumerable<Guid> tenantIds);
    
}