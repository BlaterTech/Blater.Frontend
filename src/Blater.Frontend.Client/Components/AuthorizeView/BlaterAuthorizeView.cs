using Blater.Frontend.Client.Extensions;
using Blater.Models.User;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;

using System.Security.Claims;

namespace Blater.Frontend.Client.Components.AuthorizeView;

public class BlaterAuthorizeView : ComponentBase
{
    [Parameter]
    public RenderFragment<AuthenticationState>? ChildContent { get; set; }

    [Parameter]
    public RenderFragment<AuthenticationState>? Authorized { get; set; }

    [Parameter]
    public RenderFragment<AuthenticationState>? NotAuthorized { get; set; }

    [Parameter]
    public List<string> Roles { get; set; } = [];

    [Parameter]
    public List<string> Permissions { get; set; } = [];

    /*[Parameter]
    public UserTypes UserType { get; set; } = UserTypes.None;*/

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    public BlaterUserToken GetUserAuthenticated()
    {
        return _currentAuthenticationState?.User.GetUserAuthenticated() ?? new BlaterUserToken();
    }

#nullable disable

    private AuthenticationState _currentAuthenticationState;
    private bool? _isAuthorized;

#nullable enable

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_isAuthorized.GetValueOrDefault())
        {
            var renderFragment = Authorized ?? ChildContent;
            builder.AddContent(0, renderFragment?.Invoke(_currentAuthenticationState));
        }
        else
        {
            var notAuthorized = NotAuthorized;
            var fragment = notAuthorized?.Invoke(_currentAuthenticationState);
            builder.AddContent(0, fragment);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (ChildContent != null && Authorized != null)
        {
            throw new InvalidOperationException("Do not specify both 'Authorized' and 'ChildContent'.");
        }

        if (AuthenticationState == null)
        {
            throw new InvalidOperationException("Authorization requires a cascading parameter");
        }

        _isAuthorized = new bool?();
        _currentAuthenticationState = await AuthenticationState;
        _isAuthorized = IsAuthorized(_currentAuthenticationState.User);
    }

    private bool IsAuthorized(ClaimsPrincipal user)
    {
        var blaterUserToken = user.GetUserAuthenticated();
        if (string.IsNullOrWhiteSpace(blaterUserToken.UserId))
        {
            return false;
        }

        return EnsureUserRequirements(blaterUserToken);
    }

    private bool EnsureUserRequirements(BlaterUserToken blaterUserToken)
    {
        if (Roles.Count > 0)
        {
            var hasRequiredRoles = blaterUserToken.Roles.Any(Roles.Contains);
            if (!hasRequiredRoles)
            {
                return false;
            }
        }

        if (Permissions.Count > 0)
        {
            var hasRequiredPermissions = blaterUserToken.Permissions.Any(Permissions.Contains);
            if (!hasRequiredPermissions)
            {
                return false;
            }
        }

        /*if (UserType != UserTypes.None)
        {
            var hasRequiredUserTypes = UserType == UserTypes.None;
            if (!hasRequiredUserTypes)
            {
                return false;
            }
        }*/

        return true;
    }
}