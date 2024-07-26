using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Client.Pages.Landing.Pages;

public partial class About : ComponentBase
{
    private void InitializedBreakpoints()
    {
        BrowserViewportService.DictTypo = new Dictionary<Breakpoint, (Typo t1, Typo t2)>
        {
            { Breakpoint.Xs, (Typo.h5, Typo.caption) },
            { Breakpoint.Sm, (Typo.h5, Typo.caption) },
            { Breakpoint.Md, (Typo.h5, Typo.caption) },
            { Breakpoint.Lg, (Typo.h3, Typo.body2) },
            { Breakpoint.Xl, (Typo.h3, Typo.body2) }, 
            { Breakpoint.Xxl, (Typo.h3, Typo.body2) }
        };
        
        BrowserViewportService.DictGridBreakpoint = new Dictionary<int, Dictionary<Breakpoint, string>>
        {
            {
                1, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 85vh" },
                    { Breakpoint.Sm, "height: 85vh" },
                    { Breakpoint.Md, "height: 85vh" },
                    { Breakpoint.Lg, "height: 100vh" },
                    { Breakpoint.Xl, "height: 100vh" }, 
                    { Breakpoint.Xxl, "height: 100vh" },
                }
            },
            {
                2, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 175vh" },
                    { Breakpoint.Sm, "height: 120vh" }, 
                    { Breakpoint.Md, "height: 100vh" },
                    { Breakpoint.Lg, "height: 100vh" },
                    { Breakpoint.Xl, "height: 100vh" },
                    { Breakpoint.Xxl, "height: 100vh" },
                }
            },
            {
                3, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 135vh" },
                    { Breakpoint.Sm, "height: 90vh" },
                    { Breakpoint.Md, "height: 100vh" },
                    { Breakpoint.Lg, "height: 100vh" },
                    { Breakpoint.Xl, "height: 100vh" },
                    { Breakpoint.Xxl, "height: 100vh" },
                }
            }
        };
    }
    
    protected override async Task OnInitializedAsync()
    {
        InitializedBreakpoints();
        
        var currentBreakpoint = await BrowserViewportService.GetCurrentBreakpoint().ConfigureAwait(false);
        for (var i = 0; i <= BrowserViewportService.DictGridBreakpoint.Count; i++)
        {
            BrowserViewportService.UpdateGrid(i, currentBreakpoint);
            BrowserViewportService.UpdateFonts(currentBreakpoint);
        }
    }
}