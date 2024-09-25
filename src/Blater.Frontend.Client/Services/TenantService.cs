using Blater.Enumerations;
using Blater.Frontend.Client.Contracts.Tenant;
using Blater.Frontend.Client.Interfaces;

namespace Blater.Frontend.Client.Services;

public class TenantService : ITenantService
{
    public Task<IReadOnlyList<TenantData>> Find()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TenantData>> Find(BlaterProjects projects)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> GetById(Guid tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> GetById(string tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> GetByShortName(string shortName)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> Create(TenantData tenantData)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TenantData>> Create(IEnumerable<TenantData> tenants)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> Update(TenantData tenantData)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TenantData>> Update(IEnumerable<TenantData> tenants)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> Delete(TenantData tenantData)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> Delete(Guid tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> Delete(string tenantId)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> Delete(IEnumerable<TenantData> tenants)
    {
        throw new NotImplementedException();
    }

    public Task<TenantData> Delete(IEnumerable<Guid> tenantIds)
    {
        throw new NotImplementedException();
    }
}