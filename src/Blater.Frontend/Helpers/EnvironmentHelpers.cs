using Blater.Frontend.Enumerations;

namespace Blater.Frontend.Helpers;

public static class EnvironmentHelpers
{
    static EnvironmentHelpers()
    {
        var env = Environment.GetEnvironmentVariable("BLATER_ENV") ?? "local";
        CurrentEnvironment = GetEnvironmentFromString(env);
        CurrentEnvironmentString = GetEnvironmentString(CurrentEnvironment);
    }
    
    /// <summary>
    ///     Short string for the current environment like "dev", "sta" or "prd"
    /// </summary>
    public static string CurrentEnvironmentString { get; private set; } = null!;
    
    public static Environments CurrentEnvironment { get; private set; }
    
    public static bool Is(Environments environment)
    {
        return CurrentEnvironment == environment;
    }
    
    public static void SetEnvironment(Environments env)
    {
        CurrentEnvironment = env;
        CurrentEnvironmentString = GetEnvironmentString(env);
    }
    
    public static void SetEnvironment(string env)
    {
        CurrentEnvironment = GetEnvironmentFromString(env);
        CurrentEnvironmentString = GetEnvironmentString(CurrentEnvironment);
    }
    
    public static string GetEnvironmentString(Environments env)
    {
        return env switch
        {
            Environments.Local      => "lcl",
            Environments.Test       => "tst",
            Environments.Production => "prd",
            _                       => "lcl"
        };
    }
    
    public static Environments GetEnvironmentFromString(string environment)
    {
        return environment switch
        {
            "tst"         => Environments.Test,
            "prd"         => Environments.Production,
            "Development" => Environments.Local,
            "Release"     => Environments.Production,
            "lcl"         => Environments.Local,
            _             => Environments.Local
        };
    }
    
    public static string AddEnvironmentPrefix(this string text)
    {
        return text.StartsWith($"{CurrentEnvironmentString}-") ? text : $"{CurrentEnvironmentString}-{text}";
    }
    
    public static string AddEnvironmentPrefix(this string text, string environmentString)
    {
        return text.StartsWith($"{environmentString}-") ? text : $"{environmentString}-{text}";
    }
}