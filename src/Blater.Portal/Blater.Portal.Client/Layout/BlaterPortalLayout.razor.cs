using Blater.Models.User;

namespace Blater.Portal.Client.Layout;

public partial class BlaterPortalLayout
{
    /*[Inject]
    protected NavigationService NavigationService { get; set; } = null!;*/

    //protected IEnumerable<NavMenuRouteInfo> Routes { get; set; } = [];

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
            //await AuthenticationService.TryAutoLogin();

            /*Routes = NavigationService
                    .Routes
                    .Where(x => x.RoleNames.Any(role => BlaterAuthState.RoleNames.Contains(role)))
                    .Where(x => x.Permissions.Any(permission => BlaterAuthState.Permissions.Contains(permission)));
                    */

            StateHasChanged();
        }
    }
}