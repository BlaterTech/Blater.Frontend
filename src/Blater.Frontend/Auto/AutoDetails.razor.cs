using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Models;
using Blater.Frontend.Services;
using Blater.Interfaces;
using Blater.Models;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Auto;


public partial class AutoDetails<T> where T : BaseDataModel
{
    public AutoDetails()
    {
        EditCallback = EventCallback.Factory.Create<T>(this, item => { NavigationService.Navigate($"{typeof(T).Name}/Edit/{item.Id}"); });
    }

    private string _typeName = typeof(T).Name;


    private string _title = "";

    [Parameter] public string? SubTitle { get; set; }

    [Parameter] public string? ExtraStyle { get; set; }

    [Parameter] public bool BackButton { get; set; } = true;

    [Parameter] public bool HasEditButton { get; set; } = true;

    [Parameter] public EventCallback<T> EditCallback { get; set; }

    public T? Model { get; set; }

    [Parameter]
    [EditorRequired] public BlaterId Id { get; set; } = null!;

    [Parameter] public FindPropertiesAttributeOptions<T>? Options { get; set; } = new();

    [Inject] public LocalizationService LocalizationService { get; set; } = default!;
    
    [Inject] public NavigationService NavigationService { get; set; } = default!;
    
    [Inject] public IBlaterDatabaseStoreT<T> DataRepository { get; set; } = default!;

    [Inject] public ILogger<AutoDetails<T>> Logger { get; set; } = default!;

    [Parameter] public bool Loading { get; set; } = true;

    private bool ButtonTitle => SubTitle == null;

    protected override async Task OnParametersSetAsync()
    {
        await LoadData();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadData();
        }
    }

    private async Task LoadData()
    {
        if (Id == Guid.Empty)
        {
            return;
        }

        var model = await DataRepository.FindOne(Id);

        if (model.Value == null)
        {
            Id = BlaterId.Empty;
            return;
        }

        _title = LocalizationService.Get($"Details-Title-{typeof(T).Name}");
        Model = model.Value;
        Loading = false;
        StateHasChanged();
    }

    private void EditPage()
    {
        NavigationService.Navigate($"{typeof(T).Name}/Edit/{Id}");
    }

    private async void BackPage()
    {
        await NavigationService.GoBack();
    }
}