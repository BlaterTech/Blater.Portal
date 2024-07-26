using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Client.Pages.Landing.Pages.Products;

public partial class ServerlessHosting : ComponentBase
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
                    { Breakpoint.Xs, "height: 65vh" },
                    { Breakpoint.Sm, "height: 65vh" },
                    { Breakpoint.Md, "height: 65vh" },
                    { Breakpoint.Lg, "height: 65vh" },
                    { Breakpoint.Xl, "height: 65vh" },
                    { Breakpoint.Xxl, "height: 65vh" },
                }
            },
            {
                3, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 160vh" },
                    { Breakpoint.Sm, "height: 160vh" },
                    { Breakpoint.Md, "height: 110vh" },
                    { Breakpoint.Lg, "height: 110vh" },
                    { Breakpoint.Xl, "height: 110vh" },
                    { Breakpoint.Xxl, "height: 110vh" },
                }
            },
            {
                4, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 180vh" },
                    { Breakpoint.Sm, "height: 180vh" },
                    { Breakpoint.Md, "height: 110vh" },
                    { Breakpoint.Lg, "height: 110vh" },
                    { Breakpoint.Xl, "height: 110vh" },
                    { Breakpoint.Xxl, "height: 110vh" },
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
        
        await InvokeAsync(StateHasChanged);
    }
}