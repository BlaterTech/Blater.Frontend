using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace Blater.Frontend.Client;

public static class WebSetup
{
    public static void AddBlaterFrontendClient(this IServiceCollection services)
    {
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
            config.SnackbarConfiguration.PreventDuplicates = true;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 10000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
        
        //TODO
        /*services.AddScoped<CookieHandler>();
        services.AddHttpClient<BlaterHttpClient>((_, client) =>
                 {
                     client.BaseAddress = new Uri("http://localhost:5296");
                 })
                .AddHttpMessageHandler<CookieHandler>();*/
        
        /*services.AddBlaterDatabase();
        services.AddBlaterManagement();
        services.AddBlaterKeyValue();
        services.AddBlaterAuthStores();
        services.AddBlaterAuthRepositories();*/
        
        
        services.AddBlazoredLocalStorage();
        services.AddBlazoredSessionStorage();
        
        /*services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        services.AddScoped<IBlaterStateStore, BlaterStateStore>();
        services.AddScoped<IBrowserViewportObserverService, BrowserViewportObserverService>();
        services.AddScoped<INavigationService, NavigationService>();
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IDownloadFileService, DownloadFileService>();
        services.AddScoped<LocalizationService, LocalizationService>();
        services.AddScoped<IBlobServiceClientFactory, BlobServiceClientFactory>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();
        
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<ITenantThemeService, TenantThemeService>();*/
    }
}