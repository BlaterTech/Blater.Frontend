using Blater.Frontend.Client.Contracts;

namespace Blater.Frontend.Client.Interfaces;

public interface IUserPreferencesService
{
    /// <summary>
    /// Saves UserPreferences in local storage
    /// </summary>
    /// <param name="userPreferences">The userPreferences to save in the local storage</param>
    Task SaveUserPreferences(UserPreferences? userPreferences);

    /// <summary>
    /// Loads UserPreferences in local storage
    /// </summary>
    /// <returns>UserPreferences object. Null when no settings were found.</returns>
    Task<UserPreferences?> LoadUserPreferences();
}