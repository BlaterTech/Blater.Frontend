namespace Blater.FrontEnd.Interfaces;

public interface IDownloadFileService
{
    Task DownloadFile(string file, byte[] binaryData);
}