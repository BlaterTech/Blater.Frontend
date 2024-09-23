using Blater.Frontend.Client.Enumerations;
using Blater.Frontend.Client.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

//using Blater.Frontend.Interfaces;

namespace Blater.Frontend.Client.Layouts;

public partial class BlaterPortalPrimaryLayout : LayoutComponentBase
{
    [Inject]
    protected INavigationService NavigationService { get; set; } = default!;
    
    [Inject]
    protected ILocalizationService LocalizationService { get; set; } = default!;
    
    [Inject]
    protected ILayoutService LayoutService { get; set; } = default!;
    
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
    
    public string DarkLightModeButtonText => LayoutService.CurrentDarkLightMode switch
    {
        DarkLightMode.Dark => "System mode",
        DarkLightMode.Light => "Dark mode",
        _ => "Light mode"
    };
    
    public string DarkLightModeButtonIcon => LayoutService.CurrentDarkLightMode switch
    {
        DarkLightMode.Dark => Icons.Material.Rounded.AutoMode,
        DarkLightMode.Light => Icons.Material.Outlined.DarkMode,
        _ => Icons.Material.Filled.LightMode
    };
}