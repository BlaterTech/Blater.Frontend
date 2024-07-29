using System.Diagnostics.CodeAnalysis;
using Azure.Data.Tables;
using Azure.Security.KeyVault.Secrets;
using Blater.Enumerations;
using Blater.Frontend.Extensions;
using Blater.Frontend.Interfaces;
using Blater.Frontend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Services;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
public class TenantService : ITenantService, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TenantService> _logger;
    private readonly TableClient _tableClient;

    public TenantService(SecretClient secretClient, ILogger<TenantService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        var connectionString = secretClient.GetSecretValue("blater-tenants-connectionstring");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'blater-tenants-connectionstring' not found.");
        }

        var tableServiceClient = new TableServiceClient(connectionString);
        _tableClient = tableServiceClient.GetTableClient("blatertenants");
    }

    public async Task<IReadOnlyList<TenantData>?> GetAllTenants(BlaterProjects project)
    {
        try
        {
            var queryResultsLinq =
                _tableClient.QueryAsync<TenantData>();

            var tenants = new List<TenantData>();

            await foreach (var item in queryResultsLinq)
            {
                tenants.Add(item);
            }

            return tenants.Count != 0 ? tenants : null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting all tenants");
            return null;
        }
    }

    public async Task<TenantData?> GetByShortName(string shortName)
    {
        try
        {
            var queryResultsLinq = _tableClient
               .QueryAsync<TenantData>(x => x.ShortName == shortName);

            await foreach (var item in queryResultsLinq)
            {
                return item;
            }

            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting tenant by shortname: '{@ShortName}'", shortName);
            return null;
        }
    }

    public Task<TenantData?> GetById(Guid tenantId)
    {
        return GetById(tenantId.ToString());
    }

    public async Task<TenantData?> GetById(string tenantId)
    {
        try
        {
            var queryResultsLinq = _tableClient
               .QueryAsync<TenantData>(x => x.RowKey == tenantId);

            await foreach (var item in queryResultsLinq)
            {
                return item;
            }

            return null;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting tenant by Id: '{@TenantId}'", tenantId);
            return null;
        }
    }

    public async Task<TenantData?> Create(TenantData tenantData)
    {
        try
        {
            var result = await _tableClient.AddEntityAsync(tenantData);

            return result.IsError ? null : tenantData;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while creating tenant: '{@TenantData}'", tenantData.ShortName);
            return null;
        }
    }

    public async Task<TenantData?> Update(TenantData tenantData)
    {
        try
        {
            var result = await _tableClient.UpdateEntityAsync(tenantData, tenantData.ETag);

            return result.IsError ? null : tenantData;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating tenant: '{@TenantData}'", tenantData.ShortName);
            return null;
        }
    }

    public async Task<bool> Delete(TenantData tenantData)
    {
        try
        {
            var result = await
                _tableClient.DeleteEntityAsync(tenantData.PartitionKey, tenantData.RowKey);

            return !result.IsError;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting tenant: '{@TenantData}'", tenantData.ShortName);
            return false;
        }
    }

    public async Task<TenantData?> GetCurrentTenant()
    {
        var tenantSection = _configuration.GetSection("Tenant");
        var rowKey = tenantSection.GetValue<string>("RowKey");

        return await GetById(rowKey ?? "");
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}