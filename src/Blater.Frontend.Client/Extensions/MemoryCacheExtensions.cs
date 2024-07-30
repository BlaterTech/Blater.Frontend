namespace Blater.Frontend.Client.Extensions;

public static class MemoryCacheExtensions
{
    public static string SanitizeKey(this string value)
    {
        const string key = Configuration.KeyMemoryCache;
        return !value.Contains(key) ? $"{key}-{value.ToLower(System.Globalization.CultureInfo.CurrentCulture)}" : value;
    }
}