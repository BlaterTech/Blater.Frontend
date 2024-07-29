using System.Diagnostics.CodeAnalysis;

namespace Blater.Frontend;

[SuppressMessage("Usage", "CA2211:Campos não constantes não devem ser visíveis")]
public static class Configuration
{
    public const string CookieAuthName = "Blater.Authentication";
    public const string KeyMemoryCache = "blater-state";
    public static string Jwt = "";
}