using Serilog;
using Serilog.Configuration;

namespace Blater.Frontend.Logging;

public static class LoggerEnrichmentConfigurationExtensions
{
    /// <summary>
    ///     Enrich log events with Aspnetcore httpContext properties.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <param name="serviceProvider"></param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithHttpContext(this LoggerEnrichmentConfiguration enrichmentConfiguration,
                                                      IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(enrichmentConfiguration);
        
        return enrichmentConfiguration.With(new HttpContextEnricher(serviceProvider));
    }
    
    public static LoggerConfiguration WithInvocationContext(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        ArgumentNullException.ThrowIfNull(enrichmentConfiguration);
        
        return enrichmentConfiguration.With(new InvocationContextEnricher());
    }
}