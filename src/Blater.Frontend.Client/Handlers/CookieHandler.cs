﻿using System.Net.Http.Headers;
using Blater.Frontend.Client.Extensions;

namespace Blater.Frontend.Client.Handlers;

public class CookieHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var jwt = Configuration.Jwt;

        var (isValid, _) = jwt.ValidateJwt();

        if (isValid)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}