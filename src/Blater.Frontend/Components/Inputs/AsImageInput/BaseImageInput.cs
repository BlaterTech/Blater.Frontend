using Blater.Frontend.Auto.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Blater.Frontend.Components.Inputs.AsImageInput;

public class BaseImageInput<T> : BaseComponentInput
{
    [Parameter]
    public int MaxFileSizeMb { get; set; } = 4;

    [CascadingParameter(Name = "ParentId")]
    public Guid? ParentId { get; set; }

    [Parameter]
    public string ContainerPrefix { get; set; } = "avatar";

    [Parameter]
    public bool ContainerPublic { get; set; } = true;
    
    [Parameter]
    public string? ExtraClass { get; set; }
    
    [Parameter]
    public int MinImageWidth { get; set; }

    [Parameter]
    public int MinImageHeight { get; set; }

    [Parameter]
    public int MaxImageSizeMb { get; set; }

    [Parameter]
    public double MinAspectRatio { get; set; }

    [Parameter]
    public double MaxAspectRatio { get; set; }
    
    [Parameter]
    public string? Value { get; set; }
    
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
    
    [Inject]
    public ILogger<T> Logger { get; set; } = null!;
    
    /*[Inject]
    public IBlobService BlobService { get; set; } = null!;*/
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    private const string DefaultDragClass = "z-100";
    protected string DragClass = DefaultDragClass;

    //private string ContainerName => new[]{ContainerPrefix, ParentId.ToString()}.JoinStrings("-");
    
    public Task OnInputFileChanged(InputFileChangeEventArgs e)
    {
        /*BlobService.Public = ContainerPublic;
        
        ClearDragClass();
        var file = e.GetMultipleFiles().FirstOrDefault();
        if (file == null)
        {
            Logger.LogError("File is nullable");
            return;
        }

        var maxAllowedSize = MaxFileSizeMb * 1024 * 1024;
        var fileStream = file.OpenReadStream(maxAllowedSize);

        using var image = await Image.LoadAsync(fileStream);

        if (MinAspectRatio > 0 && MaxAspectRatio > 0)
        {
            var aspectRatio = image.Width / image.Height;

            if (aspectRatio < MinAspectRatio || aspectRatio > MaxAspectRatio)
            {
                Logger.LogError("Image is not within acceptable limits.");
                Snackbar.Add("A imagem não está dentro dos limites aceitáveis.", Severity.Error);
                return;
            }
        }
        
        if (image.Width < MinImageWidth || image.Height < MinImageHeight)
        {
            Logger.LogError("Width or height do not meet minimum requirements.");
            Snackbar.Add("A largura ou a altura não atendem aos requisitos mínimos.", Severity.Error);
            return;
        }

        if (file.Size > maxAllowedSize)
        {
            Logger.LogError("The image size exceeds the maximum allowed limit.");
            Snackbar.Add("O tamanho da imagem excede o limite máximo permitido.", Severity.Error);
            return;
        }
        
        var extension = Path.GetExtension(file.Name);
        var base64 = extension switch
        {
            ".jpeg" or ".jpg" => image.ToBase64String(JpegFormat.Instance),
            ".png"            => image.ToBase64String(PngFormat.Instance),
            _                 => ""
        };

        base64 = base64.Split(',').Last();
        
        var hasUploaded = await BlobService.UploadFileBase64(ContainerName, $"avatar{extension}", base64);

        if (hasUploaded)
        {
            var url = await BlobService.GetUrlFile(ContainerName, $"avatar{extension}");
            if (url == null)
            {
                Logger.LogError("Error getting file url");
                return;
            }

            await ValueChanged.InvokeAsync(url);
        }
        */

        StateHasChanged();
        return Task.CompletedTask;
    }

    protected void SetDragClass()
    {
        DragClass = $"{DefaultDragClass} drag-enter";
    }

    protected void ClearDragClass()
    {
        DragClass = DefaultDragClass;
    }
}