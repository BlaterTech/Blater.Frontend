using System.Reflection;
using Blater.Frontend.Client.Authentication;
using Blater.Frontend.Client.Auto;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Handlers;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models.Tenant;
using Blater.Frontend.Client.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Blater.Frontend.Client;

public static class WebSetup
{
    public static void AddBlaterFrontendClient(this WebAssemblyHostBuilder builder)
    {
        AutoComponentsBuilders.Initialize();

        //builder.SetupSerilog();

        builder.Services.AddAuthorizationCore();
        builder.Services.AddCascadingAuthenticationState();
        //builder.Services.AddAuthenticationStateDeserialization();
        builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

        builder.Services.AddScoped<CookieHandler>();

        //services
        //       .AddHttpClient<BlaterHttpClient>((_, client) =>
        //        {
        //            client.BaseAddress = new Uri("http://localhost:5292");
        //        })
        //       .AddHttpMessageHandler<CookieHandler>();

        //builder.Services.AddBlaterDatabase();
        //builder.Services.AddBlaterManagement();
        //builder.Services.AddBlaterKeyValue();
        //builder.Services.AddBlaterAuthStores();
        //builder.Services.AddBlaterAuthRepositories();

        //builder.Services.AddSingleton<ICookieService, CookieService>();

        builder.Services.Configure<TenantData>(options =>
        {
            builder.Configuration
                   .GetSection(nameof(TenantData))
                   .Bind(options);
        });
        builder.Services.AddScoped<ITenantService, TenantService>();
        builder.Services.AddScoped<ITenantThemeConfigurationService, TenantThemeConfigurationService>();
        
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddSingleton<AutoConfigurations>();
        //builder.Services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        //builder.Services.AddScoped<IBlaterStateStore, BlaterStateStore>();

        //todo: creating builder.Services.AddTranslations
        builder.Services.AddSingleton<ILocalizationService, LocalizationService>();
        builder.Services.Scan(x => x.FromAssemblies(
                                         typeof(WebSetup).Assembly,
                                         Assembly.GetEntryAssembly()!,
                                         Assembly.GetExecutingAssembly(),
                                         typeof(ITranslation).Assembly)
                                    .AddClasses(classes => classes
                                                   .AssignableTo<ITranslation>())
                                    .AsImplementedInterfaces()
                                    .WithSingletonLifetime());

        builder.Services.AddScoped<INavigationService, NavigationService>();
        builder.Services.AddMudServices();
    }
}