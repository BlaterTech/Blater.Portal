using Blater.Portal.Client.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Client.Pages.LandingPage;

public partial class Home
{
    [Inject]
    protected BrowserViewportObserverService BrowserViewportService { get; set; } = null!;
    
    private void InitializedDictGridBreakpoint()
    {
        BrowserViewportService.DictGridBreakpoint = new Dictionary<int, Dictionary<Breakpoint, string>>
        {
            {
                1, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 70vh" },
                    { Breakpoint.Sm, "height: 60vh" },
                    { Breakpoint.Md, "height: 60vh" },
                    { Breakpoint.Lg, "height: 100vh" },
                    { Breakpoint.Xl, "height: 100vh" },
                    { Breakpoint.Xxl, "height: 100vh" },
                }
            },
            {
                2, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 150vh" },
                    { Breakpoint.Sm, "height: 150vh" },
                    { Breakpoint.Md, "height: 110vh" },
                    { Breakpoint.Lg, "height: 110vh" },
                    { Breakpoint.Xl, "height: 110vh" },
                    { Breakpoint.Xxl, "height: 110vh" },
                }
            },
            {
                3, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 150vh" },
                    { Breakpoint.Sm, "height: 150vh" },
                    { Breakpoint.Md, "height: 110vh" },
                    { Breakpoint.Lg, "height: 110vh" },
                    { Breakpoint.Xl, "height: 110vh" },
                    { Breakpoint.Xxl, "height: 110vh" },
                }
            },
            {
                4, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 111vh" },
                    { Breakpoint.Sm, "height: 111vh" },
                    { Breakpoint.Md, "height: 55vh" },
                    { Breakpoint.Lg, "height: 30vh" },
                    { Breakpoint.Xl, "height: 30vh" },
                    { Breakpoint.Xxl, "height: 30vh" },
                }
            },
            {
                5, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 75vh" },
                    { Breakpoint.Sm, "height: 75vh" },
                    { Breakpoint.Md, "height: 85vh" },
                    { Breakpoint.Lg, "height: 25vh" },
                    { Breakpoint.Xl, "height: 25vh" },
                    { Breakpoint.Xxl, "height: 25vh" },
                }
            }
        };
    }
    
    protected override async Task OnInitializedAsync()
    {
        InitializedDictGridBreakpoint();
        
        var currentBreakpoint = await BrowserViewportService.GetCurrentBreakpoint().ConfigureAwait(false);
        for (var i = 0; i <= BrowserViewportService.DictGridBreakpoint.Count; i++)
        {
            BrowserViewportService.UpdateGrid(i, currentBreakpoint);
        }
        
        await InvokeAsync(StateHasChanged);
    }
}