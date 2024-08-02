using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Blater.Attributes.Auto;
using Blater.Extensions;
using Blater.Frontend.Client.Attributes;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models;
using Blater.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Serilog;

namespace Blater.Frontend.Client.Services;

[SuppressMessage("Design", "CA1051:Não declarar campos de instância visíveis")]
public class NavigationService : INavigationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ILocalizationService _localizationService;
    private readonly NavigationManager _navigationManager;

    public readonly List<string> IgnorePrefixes = ["Account/"];

    public List<NavMenuRouteInfo> Routes = [];

    public NavigationService(ILocalizationService localizationService, NavigationManager navigationManager, IJSRuntime jsRuntime)
    {
        _localizationService = localizationService;
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;

        LoadRoutes();

        ILocalizationService.LocalizationChanged += LoadRoutes;
    }

    private void LoadRoutes()
    {
        Routes.Clear();

        var navMenus = (
            from type in TypesHelper.AllTypes
            let routeAttribute = type.GetCustomAttribute<RouteAttribute>()
            let autoNavMenuAttribute = type.GetCustomAttribute<AutoNavMenuAttribute>()
            let route = routeAttribute?.Template.Remove(0, 1)
            where routeAttribute != null && autoNavMenuAttribute != null && !route.StartsWithAny(IgnorePrefixes)
            orderby string.IsNullOrWhiteSpace(autoNavMenuAttribute.NavMenuParentName), autoNavMenuAttribute.Order
            select (type, route, autoNavMenuAttribute)
        ).ToList();

        foreach (var (type, route, autoNavMenuAttribute) in navMenus)
        {
            var requiredAccess = type.GetCustomAttribute<RequiredAccessAttribute>();
            //Has no parent
            if (string.IsNullOrWhiteSpace(autoNavMenuAttribute.NavMenuParentName))
            {
                Routes.Add(new NavMenuRouteInfo
                {
                    ComponentType = type,
                    Route = route,
                    Icon = autoNavMenuAttribute.Icon ?? "",
                    Name = _localizationService.GetValue($"NavMenu-{type.Name}"),
                    Priority = autoNavMenuAttribute.Order,
                    UserRoles = requiredAccess?.UserRoles,
                    UserPermissions = requiredAccess?.UserPermissions,
                    NavMenuParentName = autoNavMenuAttribute.NavMenuParentName
                });
            }
            else
            {
                //Get parent menu or create a new one if does not exists
                var parentMenuName = _localizationService.GetValue($"NavMenu-{autoNavMenuAttribute.NavMenuParentName}");
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
                        UserRoles = requiredAccess?.UserRoles,
                        UserPermissions = requiredAccess?.UserPermissions,
                    };
                    Routes.Add(parentMenu);
                }

                //Console.WriteLine($"Adding {type.Name} to {parentMenuName}");
                parentMenu.ChildRoutes.Add(new NavMenuRouteInfo
                {
                    ComponentType = type,
                    Route = route,
                    Icon = autoNavMenuAttribute.Icon ?? "",
                    Name = _localizationService.GetValue($"NavMenu-{type.Name}"),
                    Priority = autoNavMenuAttribute.Order,
                    UserRoles = requiredAccess?.UserRoles,
                    UserPermissions = requiredAccess?.UserPermissions,
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

        NavigateTo(route);
    }

    /// <summary>
    ///     Navigate to the route without any checks or adding prefixes
    /// </summary>
    /// <param name="route"></param>
    public void NavigateTo(string route)
    {
        if (route.StartsWith('/'))
        {
            route = route.Remove(0, 1);
        }

        _navigationManager.NavigateTo(route);
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