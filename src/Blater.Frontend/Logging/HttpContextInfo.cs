namespace Blater.Frontend.Logging;

public class HttpContextInfo
{
    public string? IpAddress { get; set; }

    public string Host { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public bool IsHttps { get; set; }

    public string Scheme { get; set; } = string.Empty;

    public string Method { get; set; } = string.Empty;

    public string? ContentType { get; set; }

    public string Protocol { get; set; } = string.Empty;

    public string QueryString { get; set; } = string.Empty;

    public Dictionary<string, string> Query { get; set; } = new();

    public Dictionary<string, string> Headers { get; set; } = new();

    public Dictionary<string, string> Cookies { get; set; } = new();

    public string Body { get; set; } = string.Empty;
}