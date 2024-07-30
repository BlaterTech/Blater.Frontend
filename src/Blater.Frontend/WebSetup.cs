﻿using System.Reflection;
using Blater.Frontend.Authentication;
using Blater.Frontend.Client;
using Blater.Frontend.Client.Handlers;
using Blater.Frontend.Pages.Account;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;

namespace Blater.Frontend;

public static class WebSetup
{
    public static void AddBlaterFrontendServer(this IServiceCollection services)
    {
        // Add services to the container.
        services
           .AddRazorComponents()
           .AddInteractiveServerComponents()
           .AddInteractiveWebAssemblyComponents();

        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        services
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

        services.AddHttpContextAccessor();
        //services.AddScoped<ICookieService, CookieService>();
        services.AddBlazoredLocalStorage();
        services.AddBlazoredSessionStorage();
        //services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
        //services.AddScoped<IBlaterStateStore, BlaterStateStore>();
        services.AddMudServices();
    }
    
    public static void UseBlaterFrontendServer<TApp>(this WebApplication app, params Assembly[] assemblies) where TApp : ComponentBase
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseAntiforgery();
        
        app.MapRazorComponents<TApp>()
           .AddInteractiveServerRenderMode()
           .AddInteractiveWebAssemblyRenderMode()
           .AddAdditionalAssemblies(assemblies);
    }
    
    public static async Task RunBlaterApp<TApp>(Assembly assembly, string[]? args = default)  where TApp : ComponentBase
    {
        var builder = WebApplication.CreateBuilder(args ?? []);
        
        builder.Services.AddBlaterFrontendServer();

        var app = builder.Build();
        
        app.UseBlaterFrontendServer<TApp>(assembly);
        
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