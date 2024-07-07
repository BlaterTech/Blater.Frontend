using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Blater.Attributes.Auto;
using Blater.Frontend.Interfaces;
using Blater.Frontend.Models;
using Blater.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Serilog;

namespace Blater.Frontend.Services;


public class NavigationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly LocalizationService _localizationService;
    private readonly NavigationManager _navigationManager;
    private readonly IBlaterCookieService _cookieService;

    public readonly List<string> IgnorePrefixes = ["Authentication/"];

    public List<NavMenuRouteInfo> Routes = [];

    public NavigationService(LocalizationService localizationService, NavigationManager navigationManager,
                             IJSRuntime jsRuntime, IBlaterCookieService cookieService)
    {
        _localizationService = localizationService;
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;
        _cookieService = cookieService;

        LoadRoutes();

        LocalizationService.LocalizationChanged += LoadRoutes;
    }

    private void LoadRoutes()
    {
        Routes.Clear();

        var navMenus = (
            from type in TypesHelper.AllTypes
            let routeAttribute = type.GetCustomAttribute<RouteAttribute>()
            let autoNavMenuAttribute = type.GetCustomAttribute<AutoNavMenuAttribute>()
            let route = routeAttribute?.Template.Remove(0, 1)
            where routeAttribute != null && autoNavMenuAttribute != null && !IgnorePrefixes.Any(route.StartsWith)
            orderby string.IsNullOrWhiteSpace(autoNavMenuAttribute.NavMenuParentName), autoNavMenuAttribute.Order
            select (type, route, autoNavMenuAttribute)
        ).ToList();

        foreach (var (type, route, autoNavMenuAttribute) in navMenus)
        {
            //Has no parent
            if (string.IsNullOrWhiteSpace(autoNavMenuAttribute.NavMenuParentName))
            {
                Routes.Add(new NavMenuRouteInfo
                {
                    ComponentType = type,
                    Route = route,
                    Icon = autoNavMenuAttribute.Icon ?? "",
                    Name = _localizationService.Get($"NavMenu-{type.Name}"),
                    Priority = autoNavMenuAttribute.Order,
                    RoleNames = type.GetCustomAttribute<AutoRequiredRoles>()?.RoleNames ?? [],
                    Permissions = type.GetCustomAttribute<AutoRequiredPermissions>()?.Permissions ?? [],
                    NavMenuParentName = autoNavMenuAttribute.NavMenuParentName
                });
            }
            else
            {
                //Get parent menu or create a new one if does not exists
                var parentMenuName = _localizationService.Get($"NavMenu-{autoNavMenuAttribute.NavMenuParentName}");
                var parentMenu = Routes.FirstOrDefault(x => x.Name == parentMenuName);
                if (parentMenu == null)
                {
                    // Console.WriteLine(parentMenuName);
                    parentMenu = new NavMenuRouteInfo
                    {
                        Icon = NavMenuParentIcons.Icons.GetValueOrDefault(parentMenuName) ?? Icons.Custom.Brands.Twitter,
                        Name = parentMenuName,
                        Route = $"NavMenu-{autoNavMenuAttribute.NavMenuParentName}",
                        Priority = autoNavMenuAttribute.Order,
                        RoleNames = type.GetCustomAttribute<AutoRequiredRoles>()?.RoleNames ?? [],
                        Permissions = type.GetCustomAttribute<AutoRequiredPermissions>()?.Permissions ?? [],
                    };
                    Routes.Add(parentMenu);
                }

                //Console.WriteLine($"Adding {type.Name} to {parentMenuName}");
                parentMenu.ChildRoutes.Add(new NavMenuRouteInfo
                {
                    ComponentType = type,
                    Route = route,
                    Icon = autoNavMenuAttribute.Icon ?? "",
                    Name = _localizationService.Get($"NavMenu-{type.Name}"),
                    Priority = autoNavMenuAttribute.Order,
                    RoleNames = type.GetCustomAttribute<AutoRequiredRoles>()?.RoleNames ?? [],
                    Permissions = type.GetCustomAttribute<AutoRequiredPermissions>()?.Permissions ?? [],
                    NavMenuParentName = parentMenuName
                });
            }
        }

        Routes = Routes
                .OrderBy(x => x.Priority)
                .ToList();
    }

    public void Navigate<T>() where T : ComponentBase
    {
        var route = Routes.FirstOrDefault(x => x.ComponentType == typeof(T))?.Route;

        if (route == null)
        {
            Log.Warning($"Route for {typeof(T).Name} not found");
            return;
        }

        Navigate(route);
    }

    private static string RemoveLeadingSlash(string route)
    {
        if (route.StartsWith('/'))
        {
            route = route.Remove(0, 1);
        }

        return route;
    }

    /// <summary>
    ///     Navigate to the correct page based on the current portal state, prefer the usage of the overload that uses the
    ///     component type
    /// </summary>
    public void Navigate(string route)
    {
        route = RemoveLeadingSlash(route);   
        var blaterUserToken = _cookieService.ReadCookieString("Blater-UserToken").GetAwaiter().GetResult();
        
        if (string.IsNullOrWhiteSpace(blaterUserToken))
        {
            Log.Information("Logged in is false, navigating to Authentication");
            _navigationManager.NavigateTo("login");
            return;
        }
        
        _navigationManager.NavigateTo($"{route}");
    }
    
    public void NavigateTo(string route)
    {
        route = RemoveLeadingSlash(route);
        
        _navigationManager.NavigateTo($"{route}");
    }

    public async Task GoBack()
    {
        await _jsRuntime.InvokeVoidAsync("history.back");
    }

    public async Task GoForward()
    {
        await _jsRuntime.InvokeVoidAsync("history.forward");
    }
}