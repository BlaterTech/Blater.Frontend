using Azure.Storage.Blobs;
using Blater.Frontend.Enumerations;
using Blater.Frontend.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Blater.Frontend.Factories;

public class BlobServiceClientFactory(IConfiguration configuration) : IBlobServiceClientFactory
{
    public BlobServiceClient CreateBlobServiceClient(BlobAccessLevelType blobAccessLevelType)
    {
        var name = blobAccessLevelType.ToStringFast();
        var azureConnectionString = configuration.GetConnectionString(name);
        
        if (string.IsNullOrEmpty(azureConnectionString))
        {
            throw new InvalidOperationException($"Connection string '{name}' not found");
        }
        
        return new BlobServiceClient(azureConnectionString);
    }
}