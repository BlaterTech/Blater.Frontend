using Azure.Storage.Blobs;
using Blater.Frontend.Enumerations;

namespace Blater.Frontend.Interfaces;

public interface IBlobServiceClientFactory
{
    BlobServiceClient CreateBlobServiceClient(BlobAccessLevelType blobAccessLevelType);
}