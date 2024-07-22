using System.Net.Http.Headers;
using Blater.Extensions;
using Blater.Frontend.Interfaces;
using Blater.Portal.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Blater.Portal.Client.Handlers;

public class CookieHandler(ICookieService cookieService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var result = await cookieService.GetCookie(Configuration.CookieAuthName);
        if (!string.IsNullOrWhiteSpace(result))
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            request.Headers.Add("X-Requested-With", ["XMLHttpRequest"]);
            var jwtDecoded = await result.FromBase64ToString();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtDecoded);
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}