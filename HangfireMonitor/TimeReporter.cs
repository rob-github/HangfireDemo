using Hangfire.Console;
using Hangfire.Server;

public static class TimeReporter
{
    public static async Task TellTheTime(PerformContext? context)
    {
        // create progress bar
        var progress = context.WriteProgressBar();
        await Task.Delay(TimeSpan.FromSeconds(10));
        progress.SetValue(33);

        context.WriteLine("Hangfire: It is now {0}", DateTime.Now.ToString("t"));
        await Task.Delay(TimeSpan.FromSeconds(10));
        
        progress.SetValue(66);
        
        Console.WriteLine("Console: It is now {0}", DateTime.Now.ToString("t"));
        await Task.Delay(TimeSpan.FromSeconds(10));

        // update value for previously created progress bar
        progress.SetValue(100);
    }
}