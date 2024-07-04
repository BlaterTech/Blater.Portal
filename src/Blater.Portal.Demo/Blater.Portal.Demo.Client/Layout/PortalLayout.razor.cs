using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Models;
using Blater.Frontend.Services;
using Blater.JsonUtilities;
using Blater.Models.User;
using Microsoft.AspNetCore.Components;

namespace Blater.Portal.Demo.Client.Layout;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public partial class PortalLayout
{
    [Inject]
    protected AuthenticationService AuthenticationService { get; set; } = null!;
    

    [Inject]
    protected NavigationService NavigationService { get; set; } = null!;
    
    protected IEnumerable<NavMenuRouteInfo> Routes { get; set; } = [];

    private bool _drawerOpen = true;
    private bool _loading = true;
    protected BlaterAuthState BlaterAuthState { get; set; } = new();

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var authState = await AuthenticationService.TryAutoLogin();

            if (authState != null)
            {
                BlaterAuthState = authState;
            }
            
            Routes = NavigationService
                    .Routes
                    .Where(x => x.RoleNames.Any(role => BlaterAuthState.RoleNames.Contains(role)))
                    .Where(x => x.Permissions.Any(permission => BlaterAuthState.Permissions.Contains(permission)));

            _loading = false;
            StateHasChanged();
        }
    }
}