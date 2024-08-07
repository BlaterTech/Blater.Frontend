﻿using System.Security.Claims;
using Blater.Frontend.Client;
using Blater.Frontend.Client.Extensions;
using Blater.SDK.Contracts.Authentication.Request;
using Blater.SDK.Contracts.Common.Request;
using Blater.SDK.Interfaces.BlaterAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace Blater.Frontend.Pages.Account.Pages;

public class BaseAccountPage : ComponentBase
{
    #region Props

    [CascadingParameter]
    private HttpContext HttpContext { get; set; } = default!;

    [Inject]
    private IBlaterAuthLoginStoreEndpoints LoginStore { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    private IdentityRedirectManager RedirectManager { get; set; } = default!;
    
    #endregion

    #region Methods
    
    //TODO: Criar componente para utilizar os erros

    protected async Task LoginInitialized()
    {
        if (HttpMethods.IsGet(HttpContext.Request.Method))
        {
            // Clear the existing external cookie to ensure a clean login process
            await Task.Delay(1);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
    
    protected async Task Register(string email, string name, string password)
    {
        var result = await LoginStore.Register(new RegisterBlaterUserRequest
        {
            Email = email,
            Name = name,
            Password = password
        });

        if (result.HandleErrors(out var errors, out var user))
        {
            return;
        }

        await Login(email, password);
    }

    protected async Task Login(string email, string password)
    {
        var result = await LoginStore.LoginLocal(new AuthRequest
        {
            Email = email,
            Password = password
        });

        if (result.HandleErrors(out var errors, out var jwt))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(jwt))
        {
            return;
        }

        await ReadJwtAndRedirectTo(jwt);
    }

    private async Task ReadJwtAndRedirectTo(string jwt)
    {
        var (isValid, jwtTokenDecoded) = jwt.ValidateJwt();

        if (!isValid)
        {
            return;
        }

        Configuration.Jwt = jwt;
        
        var claims = jwtTokenDecoded.Claims.ToList();
        claims.Add(new Claim("jwt", jwt));
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        RedirectManager.RedirectTo("home");
    }

    #endregion
}