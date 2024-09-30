namespace Blater.Frontend.Client.Interfaces;

public interface INavigationService
{
    /// <summary>
    ///     Navigate to the route without any checks or adding prefixes
    /// </summary>
    /// <param name="route"></param>
    void NavigateTo(string route);

    void NavigateTo(string route, Dictionary<string, object> parameters);
    void NavigateTo(string route, string paramName, object paramValue);
    Task GoBack();
    Task GoForward();
}