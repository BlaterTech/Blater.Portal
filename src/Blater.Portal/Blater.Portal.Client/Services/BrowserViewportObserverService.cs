using MudBlazor;

namespace Blater.Portal.Client.Services;

public class BrowserViewportObserverService(IBrowserViewportService browserViewportService)
{
    public Dictionary<int, Dictionary<Breakpoint, string>> DictGridBreakpoint { get; set; } = [];
    public Dictionary<int, string> DictGridStyle { get; set; } = [];
    public Dictionary<Breakpoint, (Typo t1, Typo t2)> DictTypo { get; set; } = [];

    public Typo Title;
    public Typo SubTitle;

    public async Task<Breakpoint> GetCurrentBreakpoint()
    {
        return await browserViewportService.GetCurrentBreakpointAsync().ConfigureAwait(false);
    }

    public string? GetStyleValue(int key)
    {
        return DictGridStyle.GetValueOrDefault(key);
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

    public void UpdateFonts(Breakpoint key)
    {
        if (!DictTypo.TryGetValue(key, out var value))
        {
            return;
        }

        Title = value.t1;
        SubTitle = value.t2;
    }

    public void OnBreakpointCallback(int value, Breakpoint obj)
    {
        switch (obj)
        {
            case Breakpoint.Xs:
                UpdateFonts(obj);
                UpdateGrid(value, obj);
                break;
            case Breakpoint.Sm:
            case Breakpoint.Md:
                UpdateFonts(obj);
                UpdateGrid(value, obj);
                break;
            case Breakpoint.Lg:
            case Breakpoint.Xl:
            case Breakpoint.Xxl:
                UpdateFonts(obj);
                UpdateGrid(value, obj);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
        }
    }
}