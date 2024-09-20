using Blater.Frontend.Client.Components.AuthorizeView;
using Blater.Frontend.Client.Extensions;
using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models;
using Blater.Models.User;
using Microsoft.AspNetCore.Components;

//using Blater.Frontend.Interfaces;

namespace Blater.Frontend.Client.Layouts;

public partial class BlaterPortalLayout
{
    /*[Inject]
    private IBlaterStateStore StateStore { get; set; } = null!;*/
    
    [Inject]
    private INavigationService NavigationService { get; set; } = default!;
    
    [Inject]
    private ILocalizationService LocalizationService { get; set; } = default!;

    BlaterAuthorizeView _blaterAuthorizeView = null!;
    
    private bool _drawerOpen = true;
    
    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.CompletedTask;
            var user = _blaterAuthorizeView.GetUserAuthenticated();
            var (isValid, token) = user.Jwt.ValidateJwt();
            if (isValid)
            {
                Configuration.Jwt = user.Jwt;
                //await StateStore.SetState(user);  
            }
            else
            {
                //todo: voltar ao login se jwt nao for válido
            }

            //todo: refactor this
            /*NavigationService.Routes = NavigationService
                                     .Routes
                                     .Where(x => x.UserRoles?.Any(role => BlaterAuthState.RoleNames.Contains(role)))
                                     .Where(x => x.UserPermissions?.Any(permission => BlaterAuthState.Permissions.Contains(permission)));*/
        }
    }
}