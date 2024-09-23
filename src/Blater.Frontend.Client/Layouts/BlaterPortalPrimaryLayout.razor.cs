using MudBlazor;

//using Blater.Frontend.Interfaces;

namespace Blater.Frontend.Client.Layouts;

public partial class BlaterPortalPrimaryLayout : BlaterMainLayout
{
    /*[Inject]
    private IBlaterStateStore StateStore { get; set; } = null!;*/

    //BlaterAuthorizeView _blaterAuthorizeView = null!;

    protected bool EnableHeaderToggle { get; set; } = true;
    protected bool EnableFooterToggle { get; set; }
    protected bool EnableHeaderLogo { get; set; }
    protected bool EnableDrawerLogo { get; set; } = true;

    private bool DrawerOpen { get; set; } = true;

    private void DrawerToggle()
    {
        DrawerOpen = !DrawerOpen;
    }

    /*protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.CompletedTask;
            /*var user = _blaterAuthorizeView.GetUserAuthenticated();
            var (isValid, token) = user.Jwt.ValidateJwt();
            if (isValid)
            {
                Configuration.Jwt = user.Jwt;
                //await StateStore.SetState(user);
            }
            else
            {
                //todo: voltar ao login se jwt nao for válido
            }#1#

            //todo: refactor this
            /*NavigationService.Routes = NavigationService
                                     .Routes
                                     .Where(x => x.UserRoles?.Any(role => BlaterAuthState.RoleNames.Contains(role)))
                                     .Where(x => x.UserPermissions?.Any(permission => BlaterAuthState.Permissions.Contains(permission)));#1#
        }
    }*/

    private MudThemeProvider _mudThemeProvider = null!;
    private bool _isDarkMode;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _isDarkMode = await _mudThemeProvider.GetSystemPreference();
            await _mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
            StateHasChanged();
        }
    }

    private Task OnSystemPreferenceChanged(bool newValue)
    {
        _isDarkMode = newValue;
        StateHasChanged();
        return Task.CompletedTask;
    }
}