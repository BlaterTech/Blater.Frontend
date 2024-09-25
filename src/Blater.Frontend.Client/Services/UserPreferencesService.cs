using Blater.Frontend.Client.Contracts;
using Blater.Frontend.Client.Interfaces;
using Blazored.LocalStorage;

namespace Blater.Frontend.Client.Services;

public class UserPreferencesService(ILocalStorageService localStorageService) : IUserPreferencesService
{
    private const string Key = "userPreferences";

    /// <summary>
    /// Saves UserPreferences in local storage
    /// </summary>
    /// <param name="userPreferences">The userPreferences to save in the local storage</param>
    public async Task SaveUserPreferences(UserPreferences? userPreferences)
    {
        await localStorageService.SetItemAsync(Key, userPreferences);
    }

    /// <summary>
    /// Loads UserPreferences in local storage
    /// </summary>
    /// <returns>UserPreferences object. Null when no settings were found.</returns>
    public async Task<UserPreferences?> LoadUserPreferences()
        => await localStorageService.GetItemAsync<UserPreferences>(Key);
}