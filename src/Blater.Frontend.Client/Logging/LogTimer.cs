using System.Diagnostics;
using Serilog;

namespace Blater.Frontend.Client.Logging;

public class LogTimer(string message = "") : IDisposable
{
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    
    public void Dispose()
    {
        _stopwatch.Stop();
        Print();
        GC.SuppressFinalize(this);
    }
    
    [Conditional("DEBUG")]
    private void Print()
    {
        Log.Debug("Completed {Message} in {StopwatchElapsedMilliseconds} ms", message, _stopwatch.ElapsedMilliseconds);
        //Console.WriteLine($"Completed {message} in {_stopwatch.ElapsedMilliseconds} ms");
    }
}