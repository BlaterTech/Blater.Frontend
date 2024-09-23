using System.Reflection;
using Blater.Extensions;
using Blater.Frontend.Authentication;
using Blater.Frontend.Client;
using Blater.Frontend.Client.Auto;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Handlers;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models.Tenant;
using Blater.Frontend.Client.Services;
using Blater.Frontend.Pages.Account;
using Blater.Helpers;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using MudBlazor.Services;

// ReSharper disable RedundantNameQualifier

namespace Blater.Frontend;

public static class WebSetup
{
    public static void AddBlaterFrontendServer(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services
               .AddRazorComponents()
               .AddInteractiveServerComponents()
               .AddInteractiveWebAssemblyComponents();

        //Test
        builder.Services.AddRazorPages();

        builder.Services.AddCascadingAuthenticationState();
        // builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        builder.Services
               .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
               .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Login";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.Cookie = new CookieBuilder
                    {
                        Name = Configuration.CookieAuthName,
                        IsEssential = true,
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    };
                });

        builder.Services.AddScoped<CookieHandler>();

        builder.Services.AddSingleton<TenantData>(x =>
        {
            var configuration = x.GetRequiredService<IConfiguration>();
            var tenantData = configuration.GetSection(nameof(TenantData)).Get<TenantData>();
            if (tenantData == null)
            {
                throw new Exception("TenantData not found in appSettings");
            }

            return tenantData;
        });
        
        builder.Services.AddScoped<IUserPreferencesService, UserPreferencesService>();
        builder.Services.AddScoped<ILayoutService, LayoutService>();
        
        builder.Services.AddSingleton<ITenantService, TenantService>();
        builder.Services.AddScoped<ITenantThemeConfigurationService, TenantThemeConfigurationService>();
        
        //builder.Services.AddBlaterDatabase();
        //builder.Services.AddBlaterManagement();
        //builder.Services.AddBlaterKeyValue();
        //builder.Services.AddBlaterAuthStores();
        //builder.Services.AddBlaterAuthRepositories();
        builder.Services.AddSingleton<AutoConfigurations>();
        builder.Services.AddHttpContextAccessor();
        //builder.Services.AddScoped<ICookieService, CookieService>();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredSessionStorage();
        //builder.Services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        //builder.Services.AddScoped<IBlaterStateStore, BlaterStateStore>();
        builder.Services.AddScoped<ILocalizationService, LocalizationService>();
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

    public static void UseBlaterFrontendServer<TApp>(this WebApplication app, Assembly clientAssembly) where TApp : ComponentBase
    {
        AutoComponentsBuilders.Initialize();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseAntiforgery();

        app.UseStaticFiles();
        //app.MapStaticAssets();

        var blaterFrontendAssembly = typeof(Blater.Frontend.WebSetup).Assembly;
        var blaterFrontendClientAssembly = typeof(Blater.Frontend.Client.WebSetup).Assembly;
        var executingAssembly = Assembly.GetEntryAssembly()!;

        TypesHelper.RoutesAssemblies.Add(executingAssembly); //Executing assembly AKA Server
        TypesHelper.RoutesAssemblies.Add(clientAssembly);
        TypesHelper.RoutesAssemblies.Add(blaterFrontendAssembly);
        TypesHelper.RoutesAssemblies.Add(blaterFrontendClientAssembly);

        //Test
        app.MapRazorPages();

        app.MapRazorComponents<TApp>()
           .AddInteractiveServerRenderMode()
           .AddInteractiveWebAssemblyRenderMode()
           .AddAdditionalAssemblies(blaterFrontendAssembly, blaterFrontendClientAssembly, clientAssembly);
    }
}