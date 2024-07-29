using MudBlazor;

namespace Blater.Frontend.Interfaces;

public interface ITenantThemeService
{
    MudTheme GetMudTheme(Guid tenantId);
}