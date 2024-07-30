using Blater.Enumerations;
using Blater.Frontend.Client.Authentication;
using Blater.Frontend.Client.Handlers;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Blater.Frontend.Client;

public static class WebSetup
{
    public static void AddBlaterFrontendClient(this IServiceCollection services)
    {
        //builder.SetupSerilog();

        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();
        //services.AddAuthenticationStateDeserialization();
        services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

        services.AddScoped<CookieHandler>();

        services
               .AddHttpClient<BlaterHttpClient>((_, client) =>
                {
                    client.BaseAddress = new Uri("http://localhost:5296");
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
        services.AddMudServices();
    }
    
    public static async Task RunBlaterApp(string[]? args = default)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args ?? []);
        
        builder.Services.AddBlaterFrontendClient();

        var app = builder.Build();

        try
        {
            await app.RunAsync();
        }
        finally
        {
            await app.DisposeAsync();
        }
    }
}