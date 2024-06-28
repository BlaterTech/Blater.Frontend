using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Services;
using Blater.Interfaces;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto.AutoCard;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public partial class AutoInfoCard<T> where T : BaseDataModel
{
    [Inject] private IBlaterDatabaseStoreT<T> DataRepository { get; set; } = null!;
    [Inject] private LocalizationService LocalizationService { get; set; } = null!;
    private string? Label { get; set; }

    [Parameter]
    [EditorRequired]
    public string Icon { get; set; } = null!;


    private int Counter { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Label = LocalizationService.Get($"CardLabel-{OverrideTranslation ?? typeof(T).Name}");

            await InvokeAsync(StateHasChanged);
        }
    }

    [Parameter]
    public Func<IBlaterDatabaseStoreT<T>, Task<int>>? OverrideGetCount { get; set; }

    [Parameter]
    public string? OverrideTranslation { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LocalizationService.LocalizationChanged += UpdateDataGrid;

        void UpdateDataGrid()
        {
            Label = LocalizationService.Get($"CardLabel-{OverrideTranslation ?? typeof(T).Name}");
            InvokeAsync(StateHasChanged);
        }

        if (OverrideGetCount == null)
        {
            var countFromSource = await DataRepository.Count().ConfigureAwait(false);
            Counter = countFromSource.Value;
        }
        else
        {
            var countFromSource = await OverrideGetCount(DataRepository).ConfigureAwait(false);
            Counter = countFromSource;
        }

        await InvokeAsync(StateHasChanged);
    }
}