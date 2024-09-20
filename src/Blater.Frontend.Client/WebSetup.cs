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

        //services
        //       .AddHttpClient<BlaterHttpClient>((_, client) =>
        //        {
        //            client.BaseAddress = new Uri("http://localhost:5292");
        //        })
        //       .AddHttpMessageHandler<CookieHandler>();

        //services.AddBlaterDatabase();
        //services.AddBlaterManagement();
        //services.AddBlaterKeyValue();
        //services.AddBlaterAuthStores();
        //services.AddBlaterAuthRepositories();

        //services.AddSingleton<ICookieService, CookieService>();

        services.AddSingleton<TenantData>();

        services.AddBlazoredLocalStorage();
        services.AddBlazoredSessionStorage();
        services.AddSingleton<AutoConfigurations>();
        //services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        //services.AddScoped<IBlaterStateStore, BlaterStateStore>();
        
        //todo: creating builder.Services.AddTranslations
        services.AddSingleton<ILocalizationService, LocalizationService>();
        services.Scan(x => x.FromAssemblies(
                                 typeof(WebSetup).Assembly, 
                                 Assembly.GetEntryAssembly()!,
                                 Assembly.GetExecutingAssembly(),
                                 typeof(ITranslation).Assembly)
                            .AddClasses(classes => classes
                                           .AssignableTo<ITranslation>())
                            .AsImplementedInterfaces()
                            .WithSingletonLifetime());

        services.AddScoped<INavigationService, NavigationService>();
        services.AddMudServices();
    }
}