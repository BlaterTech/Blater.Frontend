using Blater.Frontend.Client.Authentication;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Handlers;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Services;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Blater.Frontend.Client;

public static class WebSetup
{
    public static void AddBlaterFrontendClient(this IServiceCollection services)
    {
        AutoComponentsBuilders.Initialize();
        
        //builder.SetupSerilog();
        
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();
        //services.AddAuthenticationStateDeserialization();
        services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

        services.AddScoped<CookieHandler>();

        services
               .AddHttpClient<BlaterHttpClient>((_, client) =>
                {
                    client.BaseAddress = new Uri("http://localhost:5292");
                })
               .AddHttpMessageHandler<CookieHandler>();

        services.AddBlaterDatabase();
        services.AddBlaterManagement();
        services.AddBlaterKeyValue();
        services.AddBlaterAuthStores();
        services.AddBlaterAuthRepositories();

        //services.AddSingleton<ICookieService, CookieService>();
        services.AddBlazoredLocalStorage();
        services.AddBlazoredSessionStorage();
        //services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        //services.AddScoped<IBlaterStateStore, BlaterStateStore>();
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<INavigationService, NavigationService>();
        services.AddMudServices();
    }
}