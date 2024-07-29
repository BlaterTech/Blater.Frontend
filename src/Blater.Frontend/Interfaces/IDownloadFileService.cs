namespace Blater.Frontend.Interfaces;

public interface IDownloadFileService
{
    Task DownloadFile(string file, byte[] binaryData);
}