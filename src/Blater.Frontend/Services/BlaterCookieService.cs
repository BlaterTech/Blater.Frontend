using Blater.Exceptions;
using Blater.Frontend.Interfaces;
using Blater.JsonUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;

namespace Blater.Frontend.Services;

public class BlaterCookieService(
    IHttpContextAccessor httpContextAccessor,
    IJSRuntime ijsRuntime) : IBlaterCookieService
{
    public async Task<T?> ReadCookie<T>(string key)
    {
        var value = await ReadCookieString(key);
        return value.FromJson<T>();
    }

    public async Task<string> ReadCookieString(string key)
    {
        var value = "";
        
        if (httpContextAccessor.HttpContext != null)
        {
            if (!httpContextAccessor.HttpContext.Response.HasStarted)
            {
                value = httpContextAccessor.HttpContext.Request.Cookies[key];
            }
        }
        else
        {
            value = await ijsRuntime.InvokeAsync<string>("ReadCookie", key);
        }
        
        return value;
    }
    

    public Task WriteCookie<T>(string key, T value, DateTime expires)
    {
        var stringValue = value.ToJson();
        
        if (string.IsNullOrWhiteSpace(stringValue))
        {
            throw new BlaterException("Value is nullable");
        }
        
        return WriteCookieString(key, stringValue, expires);
    }

    public async Task WriteCookieString(string key, string value, DateTime expires)
    {
        if (httpContextAccessor.HttpContext != null)
        {
            if (!httpContextAccessor.HttpContext.Response.HasStarted)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Append(key, value);
                return;
            }
        }
        
        await ijsRuntime.InvokeVoidAsync("WriteCookie", key, value, expires);
    }
    

    public async Task DeleteCookie(string key)
    {
        if (httpContextAccessor.HttpContext != null)
        {
            if (!httpContextAccessor.HttpContext.Response.HasStarted)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
                return;
            }
        }
        
        await ijsRuntime.InvokeVoidAsync("DeleteCookie", key);
    }
}