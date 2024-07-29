using Azure.Security.KeyVault.Secrets;

namespace Blater.Frontend.Extensions;

public static class SecretVaultExtensions
{
    public static string? GetSecretValue(this SecretClient client, string key)
    {
        var secret = client.GetSecret(key);
        return secret.Value?.Value;
    }
    
    public static async Task<string?> GetSecretValueAsync(this SecretClient client, string key)
    {
        var secret = await client.GetSecretAsync(key);
        
        return secret.Value?.Value;
    }
}