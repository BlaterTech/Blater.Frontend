using MudBlazor;

namespace Blater.Frontend.Interfaces;

public interface IBrowserViewportObserverService
{
    Dictionary<int, Dictionary<Breakpoint, string>> DictGridBreakpoint { get; set; }
    Dictionary<int, string> DictGridStyle { get; set; }
    Dictionary<Breakpoint, (Typo t1, Typo t2)> DictTypo { get; set; }
    Task<Breakpoint> GetCurrentBreakpoint();
    string? GetStyleValue(int key);
    void UpdateGrid(int key, Breakpoint obj);
    void UpdateFonts(Breakpoint key);
    void OnBreakpointCallback(int value, Breakpoint obj);
}