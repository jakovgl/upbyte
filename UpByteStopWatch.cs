using System.Diagnostics;

namespace UpByte.Console;

public static class UpByteStopWatch
{
    private static readonly Stopwatch Stopwatch = new();
    
    public static (T, long) Execute<T>(Func<T> function)
    {
        Stopwatch.Reset();
        Stopwatch.Start();
        var result = function();
        Stopwatch.Stop();
        
        return (result, Stopwatch.ElapsedMilliseconds);
    }
}