using System.Diagnostics.CodeAnalysis;
using Blater.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blater.Portal.Client.Services;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class NavigationService(
    NavigationManager navigationManager,
    IServiceProvider serviceProvider)
{
    public void Navigate(string route)
    {
        if (route.StartsWith("/"))
        {
            route = route.Remove(0, 1);
        }
        
        var authState = serviceProvider.GetService<BlaterUserToken>();

        if (string.IsNullOrWhiteSpace(authState?.UserId))
        {
            navigationManager.NavigateTo("login");
            return;
        }

        navigationManager.NavigateTo($"{route}");
    }
}