using System.Security.Claims;
using Blater.Interfaces;
using Blater.Models;
using Blater.Models.User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Blater.Portal.Components.Account;

// This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
// every 30 minutes an interactive circuit is connected.
internal sealed class IdentityRevalidatingAuthenticationStateProvider(
    ILoggerFactory loggerFactory,
    IServiceScopeFactory scopeFactory,
    IOptions<IdentityOptions> options)
    : RevalidatingServerAuthenticationStateProvider(loggerFactory)
{
    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    protected override async Task<bool> ValidateAuthenticationStateAsync(
        AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        // Get the user manager from a new scope to ensure it fetches fresh data
        await using var scope = scopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<IBlaterDatabaseRepository<BlaterUser>>();
        return await ValidateSecurityStampAsync(userManager, authenticationState.User);
    }

    private async Task<bool> ValidateSecurityStampAsync(IBlaterDatabaseRepository<BlaterUser> repository, ClaimsPrincipal principal)
    {
        var claim = principal.Claims.FirstOrDefault(x => x.Type == "UserId");
        if (claim == null)
        {
            return false;
        }

        var user = await repository.FindOne(x => x.Id == claim.Value);
        if (user == null)
        {
            return false;
        }

        return user.Id != BlaterId.Empty;
    }
}