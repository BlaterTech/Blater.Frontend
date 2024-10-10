using Blater.Frontend.Client.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blater.Frontend.Client.Services;

public class NavigationService(NavigationManager navigationManager, IJSRuntime jsRuntime) : INavigationService
{
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

        navigationManager.NavigateTo(route);
    }

    public void NavigateTo(string route, Dictionary<string, object> parameters)
    {
        var url = navigationManager.GetUriWithQueryParameters(route, parameters!);
        navigationManager.NavigateTo(url);
    }

    public void NavigateTo(string route, string paramName, object paramValue)
    {
        var parameters = new Dictionary<string, object>
        {
            [paramName] = paramValue.ToString()!
        };

        NavigateTo(route, parameters);
    }

    public async Task GoBack()
    {
        await jsRuntime.InvokeVoidAsync("history.back");
    }

    public async Task GoForward()
    {
        await jsRuntime.InvokeVoidAsync("history.forward");
    }
}