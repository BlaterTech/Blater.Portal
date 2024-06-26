using Blater.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blater.Portal.Client.Services;

public class NavigationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigationManager;
    private readonly IServiceProvider _serviceProvider;
    
    public NavigationService(NavigationManager navigationManager,
                             IJSRuntime jsRuntime, IServiceProvider serviceProvider)
    {
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;
        _serviceProvider = serviceProvider;
    }
    
    public void Navigate(string route)
    {
        if (route.StartsWith("/"))
        {
            route = route.Remove(0, 1);
        }

        var authState = _serviceProvider.GetService<BlaterUserToken>();

        if (string.IsNullOrWhiteSpace(authState?.UserId))
        {
            _navigationManager.NavigateTo("login");
            return;
        }

        _navigationManager.NavigateTo($"{route}");
    }
}