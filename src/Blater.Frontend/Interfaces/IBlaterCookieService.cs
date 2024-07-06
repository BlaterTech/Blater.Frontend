namespace Blater.Frontend.Interfaces;

public interface IBlaterCookieService
{
    Task<T?> ReadCookie<T>(string key);
    Task<string> ReadCookieString(string key);
    
    Task WriteCookie<T>(string key, T value, DateTime expires);
    Task WriteCookieString(string key, string value, DateTime expires);
    
    Task DeleteCookie(string key);
}