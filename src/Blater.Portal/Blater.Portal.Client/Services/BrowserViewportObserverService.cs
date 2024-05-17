using MudBlazor;

namespace Blater.Portal.Client.Services;

public class BrowserViewportObserverService(
    IBrowserViewportService browserViewportService)
{
    public Dictionary<int, Dictionary<Breakpoint, string>> DictGridBreakpoint { get; set; } = new();
    public Dictionary<int, string> DictGridStyle { get; set; } = new();
    
    public Typo Title = Typo.h1;
    public Typo SubTitle = Typo.h2;
    
    public async Task<Breakpoint> GetCurrentBreakpoint()
    {
        return await browserViewportService.GetCurrentBreakpointAsync().ConfigureAwait(false);
    }
    
    public void UpdateGrid(int key, Breakpoint obj)
    {
        if (!DictGridBreakpoint.TryGetValue(key, out var value))
        {
            return;
        }
        
        if (!value.TryGetValue(obj, out var gridStyleValue))
        {
            return;
        }
        
        if (!DictGridStyle.TryAdd(key, gridStyleValue))
        {
            DictGridStyle[key] = gridStyleValue;
        }
    }
    
    private void UpdateFonts(Typo title, Typo subTitle)
    {
        Title = title;
        SubTitle = subTitle;
    }
    
    public void OnBreakpointCallback(int value, Breakpoint obj)
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
    }
}