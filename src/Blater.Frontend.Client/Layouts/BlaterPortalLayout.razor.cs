﻿using Blater.Frontend.Client.Components.AuthorizeView;
using Blater.Frontend.Client.Extensions;
//using Blater.Frontend.Interfaces;

namespace Blater.Frontend.Client.Layouts;

public partial class BlaterPortalLayout
{
    /*[Inject]
    private IBlaterStateStore StateStore { get; set; } = null!;*/

    BlaterAuthorizeView _blaterAuthorizeView = null!;
    
    private bool _drawerOpen = true;
    
    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
        StateHasChanged();
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

            /*Routes = NavigationService
                    .Routes
                    .Where(x => x.RoleNames.Any(role => BlaterAuthState.RoleNames.Contains(role)))
                    .Where(x => x.Permissions.Any(permission => BlaterAuthState.Permissions.Contains(permission)));
                    */

            StateHasChanged();
        }
    }
}