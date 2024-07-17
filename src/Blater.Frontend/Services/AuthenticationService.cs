using System.IdentityModel.Tokens.Jwt;
using Blater.Extensions;
using Blater.Frontend.Interfaces;
using Blater.Models.User;

namespace Blater.Frontend.Services;


public class AuthenticationService(
    BlaterAuthState blaterAuthState,
    IBlaterCookieService cookieService,
    NavigationService navigationService)
{
    private const string CookieKey = "Blater-UserToken";

    private async Task<LoginResponse> Login(string jwtToken, bool saveStorage = true)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var jwtTokenDecoded = jwtTokenHandler.ReadJwtToken(jwtToken);

        if (jwtTokenDecoded.ValidTo < DateTime.UtcNow)
        {
            navigationService.NavigateTo("login");
            return new LoginResponse(false, "JWT token expired");
        }

        var claims = jwtTokenDecoded.Claims
            .GroupBy(x => x.Type)
            .ToDictionary(
                g => g.Key.ToCamelCase(), 
                g => g
                    .Select(c => c.Value)
                    .ToList());

        var blaterUserToken = new BlaterUserToken();
        var props = typeof(BlaterUserToken).GetProperties();
        foreach (var prop in props)
        {
            var key = prop.Name.ToCamelCase();
            if (!claims.TryGetValue(key, out var claimValues))
            {
                continue;
            }
            
            object value;

            if (prop.Name == "LockoutEnabled")
            {
                value = claimValues.First() == "enabled";
            }
            else if(prop.PropertyType == typeof(List<string>))
            {
                value = claimValues;
            }
            else
            {
                value = claimValues.First();
            }
            
            prop.SetValue(blaterUserToken, value);
        }
        
        if (string.IsNullOrWhiteSpace(blaterUserToken.UserId))
        {
            return new LoginResponse(false, "Invalid jwt token, no UserToken claim found");
        }

        if (blaterUserToken.LockoutEnabled)
        {
            return new LoginResponse(false, "User lockout enabled");
        }

        SetState(blaterUserToken, jwtToken);

        if (saveStorage)
        {
            await cookieService.WriteCookieString(CookieKey, jwtToken, DateTime.UtcNow.AddDays(1));
        }

        navigationService.NavigateTo("home");
        return new LoginResponse(true, "User founded");
    }

    public async Task Logout()
    {
        var jwtToken = await TryGetCookie();

        if (!string.IsNullOrWhiteSpace(jwtToken))
        {
            await cookieService.DeleteCookie(CookieKey);
            blaterAuthState.LoggedIn = false;
            navigationService.NavigateTo("login");
        }
    }

    public async Task TryAutoLogin()
    {
        var jwtToken = await TryGetCookie();
        
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            navigationService.NavigateTo("login");
            return;
        }

        await Login(jwtToken);
    }
    
    public async Task<LoginResponse> LoginJwt(string jwtToken, bool saveStorage = true)
    {
        var result = await Login(jwtToken, saveStorage);

        if (result.Success)
        {
            navigationService.NavigateTo("home");
        }

        return result;
    }

    public async Task<string> TryGetCookie()
    {
        var result = await cookieService.ReadCookieString(CookieKey);
        return result;
    }
    
    private void SetState(BlaterUserToken userToken, string jwtToken)
    {
        blaterAuthState.UserId = userToken.UserId;
        blaterAuthState.Permissions = userToken.Permissions;
        blaterAuthState.RoleNames = userToken.RoleNames;
        blaterAuthState.Email = userToken.Email;
        blaterAuthState.Name = userToken.Name;
        blaterAuthState.LoggedIn = true;
        blaterAuthState.JwtToken = jwtToken;
    }
}

#pragma warning disable CA2252
public record LoginResponse(bool Success, string Message);

#pragma warning restore CA2252