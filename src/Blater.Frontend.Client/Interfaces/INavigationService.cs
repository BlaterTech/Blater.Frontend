using Blater.Frontend.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Interfaces;

public interface INavigationService
{
    public List<NavMenuRouteInfo> Routes { get; set; }
    
    void Navigate<T>() where T : ComponentBase;

    /// <summary>
    ///     Navigate to the route without any checks or adding prefixes
    /// </summary>
    /// <param name="route"></param>
    void NavigateTo(string route);

    Task GoBack();
    Task GoForward();
}