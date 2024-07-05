using Blater.Frontend.Services;
using Microsoft.AspNetCore.Components;

namespace Blater.Portal.Demo.Client.Layout;

public partial class LoginLayout
{
    [Inject]
    protected AuthenticationService AuthenticationService { get; set; } = null!;

    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await AuthenticationService.TryAutoLogin();
            StateHasChanged();
        }
    }
}