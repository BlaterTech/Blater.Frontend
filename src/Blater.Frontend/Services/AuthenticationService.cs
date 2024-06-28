using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Blater.Interfaces;
using Blater.Models.User;
using Blater.SDK.Implementations.BlaterDatabase.Stores;
using Blazored.LocalStorage;

namespace Blater.Frontend.Services;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class AuthenticationService
{

    private readonly IBlaterDatabaseStoreT<BlaterUser> _store;
    private readonly ILocalStorageService _localStorageService;
    private readonly BlaterAuthState _blaterAuthState;
    private readonly NavigationService _navigationService;
    private readonly BlaterHttpClient _blaterHttpClient;
    
    public AuthenticationService(ILocalStorageService localStorageService, BlaterAuthState blaterAuthState,
        NavigationService navigationService, BlaterHttpClient blaterHttpClient)
    {
        _localStorageService = localStorageService;
        _blaterAuthState = blaterAuthState;
        _navigationService = navigationService;
        _blaterHttpClient = blaterHttpClient;

        var databaseStore = new BlaterDatabaseStoreEndPoints(_blaterHttpClient);
        _store = new BlaterDatabaseStoreTEndPoints<BlaterUser>(databaseStore);
    }
    
    private const string LocalStorageValueKey = "Blater-UserToken";

    private async Task<LoginResponse> Login(string jwtToken, bool saveStorage = true)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        
        var jwtTokenDecoded = jwtTokenHandler.ReadJwtToken(jwtToken);
        
        var claimUserId = jwtTokenDecoded.Claims.FirstOrDefault(x => x.Type == "UserId");

        var userId = claimUserId?.Value;
        
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new LoginResponse(false, "Invalid jwt token");
        }
        
        _blaterHttpClient.SetToken(jwtToken);
        
        var findUser = await _store.FindOne(x => x.Id == userId);
        if (findUser.HandleErrors(out _, out var response))
        {
            return new LoginResponse(false, "Errors in user");
        }
        
        if (response.LockoutEnabled)
        {
            return new LoginResponse(false, "User lockout enabled");
        }

        SetUserState(response);
        
        if (saveStorage)
        {
            await _localStorageService.SetItemAsStringAsync(LocalStorageValueKey, jwtToken);
        }
        
        _navigationService.Navigate($"home");
        return new LoginResponse(true, "User founded");
    }

    public async Task Logout()
    {
        var jwt= await GetJwt();
        
        if (!string.IsNullOrWhiteSpace(jwt))
        {
            await _localStorageService.RemoveItemAsync(LocalStorageValueKey);
            _blaterAuthState.LoggedIn = false;
            _navigationService.Navigate($"login");
        }
    }
    
    public async Task TryAutoLogin()
    {
        var jwtToken = await GetJwt();
        
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            _navigationService.Navigate($"login");
            return;
        }
        
        await Login(jwtToken);
    }
    
    public async Task<string> GetJwt()
    {
        var jwtToken = await _localStorageService.GetItemAsStringAsync(LocalStorageValueKey);
        
        return string.IsNullOrWhiteSpace(jwtToken) ? string.Empty : jwtToken;
    }

    public async Task<LoginResponse> LoginJwt(string jwtToken, bool saveStorage = true)
    {
        var result = await Login(jwtToken, saveStorage);

        if (result.Success)
        {
            _navigationService.Navigate("home");
        }

        return result;
    }

    private void SetUserState(BlaterUser user)
    {
        _blaterAuthState.UserId = user.Id;
        _blaterAuthState.Email = user.Email;
        _blaterAuthState.Name = user.Name;
        _blaterAuthState.LockoutEnabled = user.LockoutEnabled;
        _blaterAuthState.RoleNames = user.RoleNames;
        _blaterAuthState.Permissions = user.Permissions;
        _blaterAuthState.LoggedIn = true;
    }
}

#pragma warning disable CA2252
public record LoginResponse(bool Success, string Message);

#pragma warning restore CA2252
