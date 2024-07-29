using Azure.Storage.Blobs;
using Blater.Frontend.Enumerations;

namespace Blater.FrontEnd.Interfaces;

public interface IBlobServiceClientFactory
{
    BlobServiceClient CreateBlobServiceClient(BlobAccessLevelType blobAccessLevelType);
}