using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models;
using Blater.Frontend.Client.Models.Tenant;

namespace Blater.Frontend.Client.Services;

public class TenantService : ITenantService
{
    public TenantData GetTenantData(Ulid id)
    {
        throw new NotImplementedException();
    }
    
    public TenantTheme GetTenantTheme(Ulid id)
    {
        throw new NotImplementedException();
    }

    public List<TenantData> GetAllTenants()
    {
        throw new NotImplementedException();
    }
}