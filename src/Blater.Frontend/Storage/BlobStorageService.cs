using System.Diagnostics.CodeAnalysis;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Blater.Extensions;
using Blater.Frontend.Enumerations;
using Blater.Frontend.Exceptions;
using Blater.Frontend.Interfaces;
using Blater.Models.Storage;
using Blater.Models.Storage.Request;
using Microsoft.Extensions.Logging;
using SharpCompress.Readers;

namespace Blater.Frontend.Storage;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
[SuppressMessage("Usage", "CA2254:O modelo deve ser uma expressão estática")]
public class BlobStorageService(
    BlobServiceClient blobServiceClient, 
    IBlobServiceClientFactory blobServiceClientFactory, 
    ILogger<BlobStorageService> logger) : IBlobStorageService, IDisposable
{
    public bool IsPublic { get; set; }
    
    public async Task<Stream?> GetFile(string container, string blobName)
    {
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
            var blobClient = blobContainer.GetBlobClient(blobName);
            
            var blobDownload = await blobClient.DownloadStreamingAsync();
            
            var memoryStream = new MemoryStream();
            
            await blobDownload.Value.Content.CopyToAsync(memoryStream);
            
            return memoryStream;
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' BlobName: '{blobName}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<string?> GetBase64File(string container, string blobName)
    {
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
            
            var blobClient = blobContainer.GetBlobClient(blobName);
            
            var blobDownload = await blobClient.DownloadAsync();
            
            var blobBase64 = await blobDownload.Value.Content.ToBase64();
            
            return blobBase64;
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' BlobName: '{blobName}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<string?> GetUrlFile(string container, string blobName)
    {
        try
        {
            if (IsPublic)
            {
                return $"https://{blobServiceClient.AccountName}.blob.core.windows.net/{container}/{blobName}";
            }
            
            var blobContainer = await GetBlobContainerClient(container);
            
            var blobClient = blobContainer.GetBlobClient(blobName);
            
            var cdnUrl = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddHours(1));
            
            return cdnUrl.ToString();
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' BlobName: '{blobName}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<bool> DeleteFile(string container, string blobName)
    {
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
            
            var blobClient = blobContainer.GetBlobClient(blobName);
            
            var deleteBlob = await blobClient.DeleteIfExistsAsync();
            
            return deleteBlob;
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' BlobName: '{blobName}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<bool> DeleteManyFiles(string container, IEnumerable<string> blobNames)
    {
        var enumerable = blobNames as string[] ?? blobNames.ToArray();
        
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
            
            await Task.WhenAll(enumerable.Select(async blobName =>
            {
                var blobClient = blobContainer.GetBlobClient(blobName);
                await blobClient.DeleteIfExistsAsync();
            }));
            
            return true;
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' Blobs: '{string.Join(',', enumerable)}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<bool> UploadFile(string container, string blobName, Stream stream)
    {
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
            
            var blobClient = blobContainer.GetBlobClient(blobName);
            
            if (blobClient == null)
            {
                throw new BlaterBlobStorageException("Error === BlobClient is null");
            }
            
            var blobContent = await blobClient.UploadAsync(stream, true);
            
            return blobContent != null;
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' BlobName: '{blobName}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<StorageFileInfo?> UploadFile(StorageFileInfoRequest file)
    {
        try
        {
            var success = await UploadFileBase64(file.Container, file.Name, file.FileContentBase64);
            
            if (!success)
            {
                return default;
            }
            
            var url = await GetUrlFile(file.Container, file.Name);
            return new StorageFileInfo
            {
                Container = file.Container,
                Extension = file.Extension,
                Name = file.Name,
                Metadata = file.Metadata,
                Url = url ?? string.Empty,
                IsPublic = IsPublic
            };
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{file.Container}' BlobName: '{file.Name}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<(string sasUri, DateTime expiresOn)> CreateSasUri(string container, string blobName)
    {
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
        
            var blobClient = blobContainer.GetBlobClient(blobName);
        
            if (blobClient == null)
            {
                throw new BlaterBlobStorageException("Error === BlobClient is null");
            }
        
            var expiresOn = DateTimeOffset.UtcNow.AddHours(8);
            var generateSasUri = blobClient.GenerateSasUri(BlobSasPermissions.PermanentDelete, expiresOn);
        
            return (generateSasUri.AbsoluteUri, expiresOn.DateTime);
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' BlobName: '{blobName}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<bool> UploadFileBase64(string container, string blobName, string toDecode)
    {
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
            
            var blobClient = blobContainer.GetBlobClient(blobName);
            
            if (blobClient == null)
            {
                throw new BlaterBlobStorageException("Error === BlobClient is null");
            }
            
            var stream = await toDecode.FromBase64ToMemoryStream();
            
            var blobContent = await blobClient.UploadAsync(stream, true);
            
            return blobContent != null;
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}' BlobName: '{blobName}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    public async Task<List<string>?> UploadManyFiles(string container, Stream fileStream)
    {
        try
        {
            var blobContainer = await GetBlobContainerClient(container);
            
            var existsContainer = await blobContainer.ExistsAsync();
            
            if (!existsContainer)
            {
                await blobContainer.CreateIfNotExistsAsync();
            }
            
            var urls = new List<string>();
            
            using var reader = ReaderFactory.Open(fileStream);
            
            while (reader.MoveToNextEntry())
            {
                if (reader.Entry.IsDirectory)
                {
                    continue;
                }
                
                var blobName = reader.Entry.Key;

                if (string.IsNullOrWhiteSpace(blobName))
                {
                    throw new BlaterBlobStorageException("Error === BlobName is null");
                }
                
                using var stream = new MemoryStream();
                reader.WriteEntryTo(stream);
                stream.Seek(0, SeekOrigin.Begin);
                
                var blobClient = blobContainer.GetBlobClient(blobName);
                
                await blobClient.UploadAsync(stream, true);
                
                var url = await GetUrlFile(container, blobName);
                
                if (!string.IsNullOrEmpty(url))
                {
                    urls.Add(url);
                }
            }
            
            return urls;
        }
        catch (Exception e)
        {
            var message = $"Error === Container: '{container}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }

    public async Task<List<StorageFileInfo>> UploadFiles(List<StorageFileInfoRequest> files)
    {
        try
        {
            var storageFileInfosUploaded = new List<StorageFileInfo>();
            foreach (var file in files)
            {
                var success = await UploadFileBase64(file.Container, file.Name, file.FileContentBase64);
                
                if (!success)
                {
                    continue;
                }
                
                var url = await GetUrlFile(file.Container, file.Name);
                storageFileInfosUploaded.Add(new StorageFileInfo
                {
                    Container = file.Container,
                    Extension = file.Extension,
                    Name = file.Name,
                    Metadata = file.Metadata,
                    Url = url ?? string.Empty,
                    IsPublic = IsPublic
                });
            }
            
            return storageFileInfosUploaded;
        }
        catch (Exception e)
        {
            var message = $"Error === " +
                          $"Containers: '{string.Join(',', files.Select(x => x.Container))}' " +
                          $"Blobs: '{string.Join(',', files.Select(x => x.Name))}'";
            logger.LogError(e, message);
            throw new BlaterBlobStorageException(message);
        }
    }
    
    private async Task<BlobContainerClient> GetBlobContainerClient(string container)
    {
        blobServiceClient = blobServiceClientFactory.CreateBlobServiceClient(IsPublic ? BlobAccessLevelType.Public : BlobAccessLevelType.Private);
        var blobContainer = blobServiceClient.GetBlobContainerClient(container);
        
        if (IsPublic)
        {
            await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);
        }
        else
        {
            await blobContainer.CreateIfNotExistsAsync();
        }
        
        return blobContainer;
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}