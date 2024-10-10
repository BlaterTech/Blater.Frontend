using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Serilog.Core;
using Serilog.Events;

using System.Text;

namespace Blater.Frontend.Logging;

public class HttpContextEnricher(IServiceProvider serviceProvider) : ILogEventEnricher
{
    private readonly IHttpContextAccessor? _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var ctx = _httpContextAccessor?.HttpContext;
        if (ctx == null)
        {
            return;
        }

        var httpContextCache = ctx.Items["serilog-enrichers-aspnetcore-httpcontext"];

        if (httpContextCache == null)
        {
            httpContextCache = StandardEnricher(_httpContextAccessor);
            ctx.Items["serilog-enrichers-aspnetcore-httpcontext"] = httpContextCache;
        }

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("HttpContext", httpContextCache, true));
    }

    public static object? StandardEnricher(IHttpContextAccessor? hca)
    {
        var ctx = hca?.HttpContext;
        if (ctx == null)
        {
            return null;
        }

        var httpContextCache = new HttpContextInfo
        {
            IpAddress = ctx.Connection.RemoteIpAddress?.ToString(),
            Host = ctx.Request.Host.ToString(),
            Path = ctx.Request.Path.ToString(),
            IsHttps = ctx.Request.IsHttps,
            Scheme = ctx.Request.Scheme,
            Method = ctx.Request.Method,
            ContentType = ctx.Request.ContentType,
            Protocol = ctx.Request.Protocol,
            QueryString = ctx.Request.QueryString.ToString(),
            Query = ctx.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Headers = ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Cookies = ctx.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString())
        };

        if (ctx.Request.ContentLength is not > 0)
        {
            return httpContextCache;
        }

        ctx.Request.EnableBuffering();

        using (var reader = new StreamReader(ctx.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            httpContextCache.Body = reader.ReadToEnd();
        }

        ctx.Request.Body.Position = 0;
        return httpContextCache;
    }
}