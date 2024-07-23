using System.Net.Http.Headers;
using Blater.Frontend.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Configuration = Blater.Frontend.Configuration;

namespace Blater.Portal.Client.Handlers;

public class CookieHandler(IServiceProvider provider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var jwt = Configuration.Jwt;

        var (isValid, _) = jwt.ValidateJwt();

        if (!isValid)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var httpClient = provider.GetRequiredService<BlaterHttpClient>();
        httpClient.SetToken(jwt);
        
        return await base.SendAsync(request, cancellationToken);
    }
}