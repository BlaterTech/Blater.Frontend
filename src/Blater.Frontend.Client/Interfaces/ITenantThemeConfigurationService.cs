using Blater.Frontend.Client.Models.Tenant;
using MudBlazor;

namespace Blater.Frontend.Client.Interfaces;

public interface ITenantThemeConfigurationService
{
    MudTheme GetMudTheme();
}