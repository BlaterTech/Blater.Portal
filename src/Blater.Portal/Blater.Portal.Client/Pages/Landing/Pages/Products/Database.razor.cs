using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Client.Pages.Landing.Pages.Products;

public partial class Database : ComponentBase
{
    private void InitializedDictGridBreakpoint()
    {
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
        InitializedDictGridBreakpoint();
        
        var currentBreakpoint = await BrowserViewportService.GetCurrentBreakpoint().ConfigureAwait(false);
        for (var i = 0; i <= BrowserViewportService.DictGridBreakpoint.Count; i++)
        {
            BrowserViewportService.UpdateGrid(i, currentBreakpoint);
        }
        
        await InvokeAsync(StateHasChanged);
    }
}