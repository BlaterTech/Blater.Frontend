using System.Reflection;
using Blater.Frontend.Client.Authentication;
using Blater.Frontend.Client.Auto;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Handlers;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
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
        
        builder.Services.AddScoped<IUserPreferencesService, UserPreferencesService>();
        builder.Services.AddScoped<ILayoutService, LayoutService>();

        builder.Services.AddSingleton<ITenantService, TenantService>();
        builder.Services.AddScoped<ITenantThemeConfigurationService, TenantThemeConfigurationService>();

        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddSingleton<AutoConfigurations>();
        //builder.Services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        //builder.Services.AddScoped<IBlaterStateStore, BlaterStateStore>();

        //todo: creating builder.Services.AddTranslations
        var blaterFrontendClientAssembly = typeof(WebSetup).Assembly;
        var executingAssembly = Assembly.GetEntryAssembly()!;
        builder.Services.AddScoped<ILocalizationService, LocalizationService>();
        builder.Services.Scan(x => x.FromAssemblies(
                                         blaterFrontendClientAssembly,
                                         executingAssembly)
                                    .AddClasses(classes => classes
                                                   .AssignableTo<ITranslation>())
                                    .AsImplementedInterfaces()
                                    .WithSingletonLifetime());

        builder.Services.AddScoped<INavigationService, NavigationService>();
        builder.Services.AddMudServices(config =>
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
    }
}