using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Models;
using Blater.Frontend.Services;
using Blater.JsonUtilities;
using Blater.Models.User;
using Microsoft.AspNetCore.Components;

namespace Blater.Portal.Client.Layout;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public partial class PortalLayout
{
    [Inject]
    protected AuthenticationService AuthenticationService { get; set; } = null!;
    
    [Inject]
    protected BlaterAuthState BlaterAuthState { get; set; } = null!;    

    [Inject]
    protected NavigationService NavigationService { get; set; } = null!;
    
    protected IEnumerable<NavMenuRouteInfo> Routes { get; set; } = [];

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
             await AuthenticationService.TryAutoLogin();

            Routes = NavigationService
                    .Routes
                    .Where(x => x.RoleNames.Any(role => BlaterAuthState.RoleNames.Contains(role)))
                    .Where(x => x.Permissions.Any(permission => BlaterAuthState.Permissions.Contains(permission)));
            
            await InvokeAsync(StateHasChanged);
        }
    }
}