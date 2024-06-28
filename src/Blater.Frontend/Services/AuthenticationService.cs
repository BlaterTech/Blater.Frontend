using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Blater.JsonUtilities;
using Blater.Models.User;
using Blazored.LocalStorage;

namespace Blater.Frontend.Services;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class AuthenticationService(
    ILocalStorageService localStorageService,
    BlaterAuthState blaterAuthState,
    NavigationService navigationService)
{
    private const string LocalStorageValueKey = "Blater-UserToken";

    private async Task<LoginResponse> Login(string jwtToken, bool saveStorage = true)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        
        var jwtTokenDecoded = jwtTokenHandler.ReadJwtToken(jwtToken);

        if (jwtTokenDecoded.ValidTo < DateTime.UtcNow)
        {
            navigationService.Navigate("login");
            return new LoginResponse(false, "JWT token expired");
        }

        var userTokenClaim = jwtTokenDecoded.Claims.FirstOrDefault(x => x.Type == "UserToken");

        if (userTokenClaim == null)
        {
            return new LoginResponse(false, "Invalid jwt token, no UserToken claim found");
        }
        
        var blaterUserToken = userTokenClaim.Value.FromJson<BlaterUserToken>();

        if (blaterUserToken == null)
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
            await localStorageService.SetItemAsStringAsync(LocalStorageValueKey, jwtToken);
        }
        
        navigationService.Navigate("home");
        return new LoginResponse(true, "User founded");
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

    public async Task Logout()
    {
        var jwt= await GetJwt();
        
        if (!string.IsNullOrWhiteSpace(jwt))
        {
            await localStorageService.RemoveItemAsync(LocalStorageValueKey);
            blaterAuthState.LoggedIn = false;
            navigationService.Navigate("login");
        }
    }
    
    public async Task TryAutoLogin()
    {
        var jwtToken = await GetJwt();
        
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            navigationService.Navigate("login");
            return;
        }
        
        await Login(jwtToken);
    }
    
    public async Task<string> GetJwt()
    {
        var jwtToken = await localStorageService.GetItemAsStringAsync(LocalStorageValueKey);
        
        return string.IsNullOrWhiteSpace(jwtToken) ? string.Empty : jwtToken;
    }

    public async Task<LoginResponse> LoginJwt(string jwtToken, bool saveStorage = true)
    {
        var result = await Login(jwtToken, saveStorage);

        if (result.Success)
        {
            navigationService.Navigate("home");
        }

        return result;
    }
}

#pragma warning disable CA2252
public record LoginResponse(bool Success, string Message);

#pragma warning restore CA2252
