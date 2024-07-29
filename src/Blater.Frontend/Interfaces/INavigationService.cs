using Microsoft.AspNetCore.Components;

namespace Blater.FrontEnd.Interfaces;

public interface INavigationService
{
    void Navigate<T>() where T : ComponentBase;

    /// <summary>
    ///     Navigate to the route without any checks or adding prefixes
    /// </summary>
    /// <param name="route"></param>
    void NavigateTo(string route);

    Task GoBack();
    Task GoForward();
}