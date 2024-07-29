using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Interfaces;
using Blater.JsonUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;

namespace Blater.Frontend.Services;

[SuppressMessage("Usage", "CA2201:Não gerar tipos de exceção reservados")]
public class CookieService(IJSRuntime jsRuntime, IHttpContextAccessor httpContextAccessor) : ICookieService
{
    private readonly HttpContext? _httpContext = httpContextAccessor.HttpContext;

    public async Task<T?> GetCookie<T>(string key)
    {
        var result = await GetCookie(key);
        var value = result.FromJson<T>();
        return value;
    }

    public async Task<string> GetCookie(string key)
    {
        string result;

        try
        {

        }
        catch
        {
            throw;
        }
    
        if (_httpContext is { Response.HasStarted: false })
        {
            result = _httpContext.Request.Cookies[key];
        }
        else
        {
            result = await jsRuntime.InvokeAsync<string>("ReadCookie", key);
        }

        return result;
    }

    public async Task<T> SetCookie<T>(string key, T value)
    {
        var stringValue = value.ToJson();
        if (string.IsNullOrWhiteSpace(stringValue))
        {
            throw new Exception("Value is nullable");
        }

        var result = await SetCookie(key, stringValue);

        var setValue = result.FromJson<T>();
        if (setValue == null)
        {
            throw new Exception($"Error in set value: '{stringValue}' in cookie key: '{key}'");
        }

        return setValue;
    }

    public async Task<string> SetCookie(string key, string value)
    {
        if (_httpContext is { Response.HasStarted: false })
        {
            _httpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                IsEssential = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.MaxValue,
                Secure = true
            });
        }
        else
        {
            await jsRuntime.InvokeVoidAsync("WriteCookie", key, value, DateTime.MaxValue);
        }

        var result = await GetCookie(key);

        return result;
    }

    public async Task<bool> DeleteCookie(string key)
    {
        if (_httpContext is { Response.HasStarted: false })
        {
            _httpContext.Response.Cookies.Delete(key);
        }
        else
        {
            await jsRuntime.InvokeVoidAsync("DeleteCookie", key);
        }

        var result = await GetCookie(key);

        return string.IsNullOrWhiteSpace(result);
    }
}