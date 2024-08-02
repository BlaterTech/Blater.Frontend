using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Blater.Helpers;
using Microsoft.Extensions.Hosting;

namespace Blater.Frontend.Client.Logging;

[SuppressMessage("Globalization", "CA1305:Especificar IFormatProvider")]
public static class LoggingExtensions
{
    private static readonly ConsoleTheme Theme = new SystemConsoleTheme(
        new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
        {
            [ConsoleThemeStyle.Text] = new()
            {
                Foreground = ConsoleColor.Gray
            },
            [ConsoleThemeStyle.SecondaryText] = new()
            {
                Foreground = ConsoleColor.DarkGray
            },
            [ConsoleThemeStyle.TertiaryText] = new()
            {
                Foreground = ConsoleColor.DarkGray
            },
            [ConsoleThemeStyle.Invalid] = new()
            {
                Foreground = ConsoleColor.Yellow
            },
            [ConsoleThemeStyle.Null] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Name] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.String] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Number] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Boolean] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Scalar] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.LevelVerbose] = new()
            {
                Foreground = ConsoleColor.Gray,
                Background = ConsoleColor.DarkGray
            },
            [ConsoleThemeStyle.LevelDebug] = new()
            {
                Foreground = ConsoleColor.Magenta
            },
            [ConsoleThemeStyle.LevelInformation] = new()
            {
                Foreground = ConsoleColor.Blue
            },
            [ConsoleThemeStyle.LevelWarning] = new()
            {
                Foreground = ConsoleColor.Yellow
            },
            [ConsoleThemeStyle.LevelError] = new()
            {
                Foreground = ConsoleColor.White,
                Background = ConsoleColor.Red
            },
            [ConsoleThemeStyle.LevelFatal] = new()
            {
                Foreground = ConsoleColor.White,
                Background = ConsoleColor.Red
            }
        });
    
    public static void SetupBootstrapLogger()
    {
        Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", Assembly.GetExecutingAssembly().GetName().Name)
                    .Enrich.WithProperty("Environment", EnvironmentHelpers.CurrentEnvironmentString)
                    .MinimumLevel.Debug()
                    .WriteTo.Console(
                         LogEventLevel.Debug,
                         theme: Theme,
                         outputTemplate:
                         "[{Timestamp:HH:mm:ss.fff}] [{Level:u3}] {Message} ({SourceContext}){NewLine}{Exception}")
                    .WriteTo.File("logs/bootstrap.txt",
                                  LogEventLevel.Debug,
                                  "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] ({SourceContext}) {Message}{NewLine}START{Exception}END{NewLine}",
                                  rollingInterval: RollingInterval.Day,
                                  fileSizeLimitBytes: 4096 * 1024 * 2,
                                  rollOnFileSizeLimit: true)
                    .CreateBootstrapLogger();
    }
    
    public static void UseSerilogDefaultConfig(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, sp, config) => { config.ConfigLoggerConfig(sp); });
    }
    
    public static void UseSerilogDefaultConfig(this HostApplicationBuilder builder)
    {
        builder.Services.AddSerilog((sp, config) => config.ConfigLoggerConfig(sp));
    }
    
    /*public static void UseSerilogDefaultConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddSerilog((sp, config) => config.ConfigLoggerConfig(sp));
    }*/
    
    private static LoggerConfiguration ConfigLoggerConfig(this LoggerConfiguration config, IServiceProvider sp)
    {
        return config.Enrich.FromLogContext()
                     .Enrich.WithProperty("ApplicationName", Assembly.GetExecutingAssembly().GetName().Name)
                     .Enrich.WithProperty("Environment", EnvironmentHelpers.CurrentEnvironmentString)
                     .Enrich.WithCorrelationId()
                     .Enrich.WithExceptionDetails()
                     .Enrich.WithHttpContext(sp)
                     .Enrich.WithInvocationContext()
                     .MinimumLevel.Debug()
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                     .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                     .MinimumLevel.Override("MudBlazor", LogEventLevel.Warning)
                     .WriteTo.Console(
                          LogEventLevel.Debug,
                          theme: Theme,
                          outputTemplate:
                          "[{Timestamp:HH:mm:ss.fff}] [{Level:u3}] {Message} ({SourceContext})|{CallerLineNumber}|{NewLine}{Exception}")
                     .WriteTo.File("logs/log.txt",
                                   LogEventLevel.Debug,
                                   "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] ({SourceContext})|{CallerLineNumber}| {Message}{NewLine}START{Exception}END{NewLine}",
                                   rollingInterval: RollingInterval.Day,
                                   fileSizeLimitBytes: 4096 * 1024 * 2,
                                   rollOnFileSizeLimit: true);
    }
}