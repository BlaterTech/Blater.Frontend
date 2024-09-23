using Blater.Frontend.Client.Models.Tenant;
using MudBlazor;

namespace Blater.Frontend.Client.Interfaces;

public interface ITenantThemeConfigurationService
{
    MudTheme DefaultTheme { get; set; }
    bool IsDarkMode { get; }
    MudTheme GetMudTheme();
    TenantTheme GetTenantTheme();
}