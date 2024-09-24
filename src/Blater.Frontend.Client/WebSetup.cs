using System.Reflection;
using Blater.Enumerations;
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

        //todo: verificar como será obtido os tenants
        builder.Services.AddSingleton(new TenantData
        {
            TenantId = Guid.Parse("08dcd9b0-26be-f5f5-a6e1-6d3612fba317"),
            Name = "Juriself",
            Domain = "example.com",
            SubDomain = "sub.example.com",
            ShortName = "js",
            Project = BlaterProjects.Juriself,
            MultipleTenants = false,
            CanCreateAccountOAuth = true,
            TenantTheme = new TenantTheme
            {
                NavMenuLogo = "https://example.com/logo.png",
                Favicon = "https://example.com/favicon.ico",
                LoginLogo = "https://example.com/loginlogo.png",
                LoginBackgroundImage = "https://example.com/background.png",
                IsDarkMode = false,
                PaletteDark = new TenantPaletteDark
                {
                    Primary = "#6F1677",
                    PrimaryDarken = "#510358",
                    PrimaryLighten = "#913099",
                    Secondary = "#E1B7E5",
                    SecondaryDarken = "#C78DCC",
                    SecondaryLighten = "#F3DAF5",
                    Tertiary = "#ED7005",
                    TertiaryDarken = "#BD5700",
                    TertiaryLighten = "#FBAA65",
                    DrawerBackground = "#6F1677",
                    DrawerText = "#FFFFFF",
                    DrawerIcon = "#FFFFFF",
                    AppbarBackground = "#FFFFFF",
                    AppbarText = "#FFFFFF"
                },
                PaletteLight = new TenantPaletteLight
                {
                    Primary = "#6F1677",
                    PrimaryDarken = "#510358",
                    PrimaryLighten = "#913099",
                    Secondary = "#E1B7E5",
                    SecondaryDarken = "#C78DCC",
                    SecondaryLighten = "#F3DAF5",
                    Tertiary = "#ED7005",
                    TertiaryDarken = "#BD5700",
                    TertiaryLighten = "#FBAA65",
                    DrawerBackground = "#6F1677",
                    DrawerText = "#FFFFFF",
                    DrawerIcon = "#FFFFFF",
                    AppbarBackground = "#FFFFFF",
                    AppbarText = "#000000"
                },
                Typography = new TenantTypography
                {
                    Button = new TenantTypographyButton
                    {
                        FontWeight = 700,
                        FontSize = "0.85rem"
                    }
                }
            }
        });
        
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