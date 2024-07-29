using Blater.Frontend.Factories;
using Blater.Frontend.Handlers;
using Blater.Frontend.Interfaces;
using Blater.Frontend.Logging;
using Blater.Frontend.Pages.Account;
using Blater.Frontend.Persisting.Authentication;
using Blater.Frontend.Services;
using Blater.Frontend.StateManagement;
using Blater.Frontend.StateManagement.Database;
using Blater.Frontend.Storage;
using Blater.Frontend.Tenant;
using Blater.Logging;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace Blater.Frontend;

public static class WebSetup
{
    public static void AddBlaterFrontendServices(this IServiceCollection services)
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
        
        services.AddScoped<CookieHandler>();
        services.AddHttpClient<BlaterHttpClient>((_, client) => { client.BaseAddress = new Uri("http://localhost:5296"); })
                .AddHttpMessageHandler<CookieHandler>();
        
        services.AddBlaterDatabase();
        services.AddBlaterManagement();
        services.AddBlaterKeyValue();
        services.AddBlaterAuthStores();
        services.AddBlaterAuthRepositories();
        
        
        services.AddBlazoredLocalStorage();
        services.AddBlazoredSessionStorage();
        
        services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        services.AddScoped<IBlaterStateStore, BlaterStateStore>();
        services.AddScoped<IBrowserViewportObserverService, BrowserViewportObserverService>();
        services.AddScoped<INavigationService, NavigationService>();
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IDownloadFileService, DownloadFileService>();
        services.AddScoped<LocalizationService, LocalizationService>();
        services.AddScoped<IBlobServiceClientFactory, BlobServiceClientFactory>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();
        
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<ITenantThemeService, TenantThemeService>();
    }
}