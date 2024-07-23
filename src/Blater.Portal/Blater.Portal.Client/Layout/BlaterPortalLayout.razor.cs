using Blater.Frontend;
using Blater.Frontend.Extensions;
using Blater.Frontend.Interfaces;
using Blater.Models.User;
using Blater.Portal.Client.Components.AuthorizeView;
using Microsoft.AspNetCore.Components;

namespace Blater.Portal.Client.Layout;

public partial class BlaterPortalLayout
{
    [Inject]
    private IBlaterStateStore StateStore { get; set; } = null!;

    BlaterAuthorizeView _blaterAuthorizeView;
    
    private bool _drawerOpen = true;
    private bool _loading;
    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var user = _blaterAuthorizeView.GetUserAuthenticated();
            var (isValid, token) = user.Jwt.ValidateJwt();
            if (isValid)
            {
                Configuration.Jwt = user.Jwt;
                await StateStore.SetState(user);  
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