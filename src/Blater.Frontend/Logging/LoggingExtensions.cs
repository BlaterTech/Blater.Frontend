using System.Reflection;
using Blater.Frontend.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace Blater.Frontend.Logging;

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
    
    public static void SetupSerilog(this WebAssemblyHostBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", Assembly.GetExecutingAssembly().GetName().Name)
            .Enrich.WithProperty("Environment", EnvironmentHelpers.CurrentEnvironmentString)
            .Enrich.WithCorrelationId()
            .Enrich.WithExceptionDetails()
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
            .CreateLogger();

        builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
    }
}