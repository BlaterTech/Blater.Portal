using MudBlazor;

namespace Blater.Portal.Client.Pages;

public partial class Home
{
    private Typo _title = Typo.h1;
    private Typo _subTitle = Typo.h2;
    private string _firstGridStyle = "";
    private void OnBreakpointCallback(Breakpoint obj)
    {
        int i = 1;
        switch (obj)
        {
            case Breakpoint.Xs:
                _title = Typo.h4;
                _subTitle = Typo.h5;
                _firstGridStyle = "height: 70vh";
                break;
            case Breakpoint.Sm:
                _title = Typo.h3;
                _subTitle = Typo.h4;
                _firstGridStyle = "height: 50vh";
                break;
            case Breakpoint.Md:
                _title = Typo.h3;
                _subTitle = Typo.h4;
                _firstGridStyle = "height: 60vh";
                break;
            case Breakpoint.Lg:
            case Breakpoint.Xl:
            case Breakpoint.Xxl:
                _title = Typo.h2;
                _subTitle = Typo.h3;
                _firstGridStyle = "height: 95vh";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
        }
    }
}