using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Client.Pages;

public partial class Home
{
    [Inject]
    protected IBrowserViewportService BrowserViewportService { get; set; } = null!;
    
    private Typo _title = Typo.h1;
    private Typo _subTitle = Typo.h2;
    private Dictionary<int, Dictionary<Breakpoint, string>> _dictGridBreakpoint = new();
    private readonly Dictionary<int, string> _dictGridStyle = new();
    
    private void InitializedDictGridBreakpoint()
    {
        _dictGridBreakpoint = new Dictionary<int, Dictionary<Breakpoint, string>>
        {
            {
                1, new Dictionary<Breakpoint, string>
                {
                    { Breakpoint.Xs, "height: 70vh" },
                    { Breakpoint.Sm, "height: 60vh" },
                    { Breakpoint.Md, "height: 60vh" },
                    { Breakpoint.Lg, "height: 95vh" },
                    { Breakpoint.Xl, "height: 95vh" },
                    { Breakpoint.Xxl, "height: 95vh" },
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
                    { Breakpoint.Xs, "height: 65vh" },
                    { Breakpoint.Sm, "height: 65vh" },
                    { Breakpoint.Md, "height: 65vh" },
                    { Breakpoint.Lg, "height: 65vh" },
                    { Breakpoint.Xl, "height: 65vh" },
                    { Breakpoint.Xxl, "height: 65vh" },
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
    
    private void UpdateGrid(int key, Breakpoint obj)
    {
        if (!_dictGridBreakpoint.TryGetValue(key, out var value))
        {
            return;
        }
        
        if (!value.TryGetValue(obj, out var gridStyleValue))
        {
            return;
        }
        
        if (!_dictGridStyle.TryAdd(key, gridStyleValue))
        {
            _dictGridStyle[key] = gridStyleValue;
        }
    }
    
    protected override async Task OnInitializedAsync()
    {
        InitializedDictGridBreakpoint();
        
        var currentBreakpoint = await BrowserViewportService.GetCurrentBreakpointAsync().ConfigureAwait(false);
        for (var i = 0; i <= 5; i++)
        {
            UpdateGrid(i, currentBreakpoint);
        }
        
        await InvokeAsync(StateHasChanged);
    }
    
    private void UpdateFonts(Typo title, Typo subTitle)
    {
        _title = title;
        _subTitle = subTitle;
    }
    
    private void OnBreakpointCallback(int value, Breakpoint obj)
    {
        switch (obj)
        {
            case Breakpoint.Xs:
                UpdateFonts(Typo.h4, Typo.h5);
                UpdateGrid(value, obj);
                break;
            case Breakpoint.Sm:
            case Breakpoint.Md:
                UpdateFonts(Typo.h3, Typo.h4);
                UpdateGrid(value, obj);
                break;
            case Breakpoint.Lg:
            case Breakpoint.Xl:
            case Breakpoint.Xxl:
                UpdateFonts(Typo.h2, Typo.h3);
                UpdateGrid(value, obj);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
        }
        
        StateHasChanged();
    }
    
}