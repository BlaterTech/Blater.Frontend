using Blater.Results;

namespace Blater.Frontend.Interfaces;

public interface ICookieService
{
    Task<T?> GetCookie<T>(string key);
    Task<string> GetCookie(string key);
    
    Task<T> SetCookie<T>(string key, T value);
    Task<string> SetCookie(string key, string value);
    
    Task<bool> DeleteCookie(string key);
}