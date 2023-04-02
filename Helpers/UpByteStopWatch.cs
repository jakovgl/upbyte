using System.Diagnostics;

namespace UpByte.Console;

public static class UpByteStopWatch
{
    public static (T, long) Execute<T>(Func<T> function)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var result = function();
        stopWatch.Stop();

        return (result, stopWatch.ElapsedMilliseconds);
    }
}