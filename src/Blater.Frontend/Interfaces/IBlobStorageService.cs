using Blater.Models.Storage;
using Blater.Models.Storage.Request;

namespace Blater.Frontend.Interfaces;

public interface IBlobStorageService
{
    bool IsPublic { get; set; }
    Task<Stream?> GetFile(string container, string blobName);
    Task<string?> GetBase64File(string container, string blobName);
    Task<string?> GetUrlFile(string container, string blobName);
    Task<bool> DeleteFile(string container, string blobName);
    Task<bool> DeleteManyFiles(string container, IEnumerable<string> blobNames);
    Task<bool> UploadFile(string container, string blobName, Stream stream);
    Task<StorageFileInfo?> UploadFile(StorageFileInfoRequest file);
    Task<(string sasUri, DateTime expiresOn)> CreateSasUri(string container, string blobName);
    Task<bool> UploadFileBase64(string container, string blobName, string toDecode);
    Task<List<string>?> UploadManyFiles(string container, Stream fileStream);
    Task<List<StorageFileInfo>> UploadFiles(List<StorageFileInfoRequest> files);
    void Dispose();
}