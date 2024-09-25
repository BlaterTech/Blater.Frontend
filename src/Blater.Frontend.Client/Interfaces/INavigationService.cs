﻿using Blater.Frontend.Client.Contracts;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Interfaces;

public interface INavigationService
{
    List<NavMenuRouteInfo> Routes { get; set; }
    Dictionary<string, (List<string> roles, List<string> permissions)> RouteAuthorizations { get; set; }
    void Navigate<T>() where T : ComponentBase;

    /// <summary>
    ///     Navigate to the route without any checks or adding prefixes
    /// </summary>
    /// <param name="route"></param>
    void NavigateTo(string route);

    Task GoBack();
    Task GoForward();
}