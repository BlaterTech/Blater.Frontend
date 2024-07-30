using System.Diagnostics;
using System.Security.Claims;
using Blater.Frontend.Client.Extensions;
using Blater.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace Blater.Frontend.Authentication;

// This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
// every 30 minutes an interactive circuit is connected. It also uses PersistentComponentState to flow the
// authentication state to the client which is then fixed for the lifetime of the WebAssembly application.
public sealed class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
    private readonly PersistentComponentState _state;
    private readonly PersistingComponentStateSubscription _subscription;

    private Task<AuthenticationState>? _authenticationStateTask;

    public PersistingRevalidatingAuthenticationStateProvider(
        ILoggerFactory loggerFactory,
        PersistentComponentState persistentComponentState)
        : base(loggerFactory)
    {
        _state = persistentComponentState;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        _subscription = _state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        return Task.FromResult(ValidateSecurityStampAsync(authenticationState.User));
    }

    private static bool ValidateSecurityStampAsync(ClaimsPrincipal principal)
    {
        var claim = principal.Claims.FirstOrDefault(x => x.Type == "UserId");
        if (claim == null)
        {
            return false;
        }
        
        var token = principal.Claims.FirstOrDefault(x => x.Type == "UserToken")?.Value;
        var valid = token?.ValidateJwt();

        return valid?.isValid ?? false;
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        _authenticationStateTask = task;
    }

    private async Task OnPersistingAsync()
    {
        if (_authenticationStateTask is null)
        {
            throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
        }

        var authenticationState = await _authenticationStateTask;
        var principal = authenticationState.User;

        if (principal.Identity?.IsAuthenticated == true)
        {
            var blaterUserToken = principal.GetUserAuthenticated();
            if (!string.IsNullOrWhiteSpace(blaterUserToken.UserId))
            {
                _state.PersistAsJson(nameof(BlaterUserToken), blaterUserToken);
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
        base.Dispose(disposing);
    }
}