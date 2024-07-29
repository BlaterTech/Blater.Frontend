using System.Diagnostics.CodeAnalysis;
using Blater.FrontEnd.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Blater.Frontend.Services;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
public class DownloadFileService(IJSRuntime jsRuntime, ILogger<DownloadFileService> logger) : IDownloadFileService
{
    public async Task DownloadFile(string file, byte[] binaryData)
    {
        try
        {
            using var memoryStream = new MemoryStream(binaryData);
        
            using var streamRef = new DotNetStreamReference(stream: memoryStream);
        
            await jsRuntime.InvokeVoidAsync(
                "downloadFileFromStream",
                file,
                streamRef);
        }
        catch (Exception e)
        {
            logger.LogError(e,"Error in DownloadFileFromStream File: '{@File}'", file);
        }
    }
}