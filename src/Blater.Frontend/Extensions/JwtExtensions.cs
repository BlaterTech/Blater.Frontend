using System.IdentityModel.Tokens.Jwt;

namespace Blater.Frontend.Extensions;

public static class JwtExtensions
{
    public static (bool isValid, JwtSecurityToken token) ValidateJwt(this string jwt)
    {
        if (string.IsNullOrWhiteSpace(jwt))
        {
            return (false, new JwtSecurityToken());
        }
        
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        
        var jwtTokenDecoded = jwtTokenHandler.ReadJwtToken(jwt);

        if (jwtTokenDecoded.ValidTo.ToUniversalTime() <= DateTime.UtcNow)
        {
            return (false, jwtTokenDecoded);
        }

        return (true, jwtTokenDecoded);
    }
}