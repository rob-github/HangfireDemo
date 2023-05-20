using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace HangfireMonitor.Jobs
{
    public class CountTillTenJob : IRegisterJob
    {
        public void RegisterJob()
        {
            RecurringJob.AddOrUpdate<CountTillTenJob>(
                $"{nameof(CountTillTenJob)}",
                job => job.Execute(null),
                Cron.MinuteInterval(3));
        }

        public async Task<string> Execute(PerformContext context)
        {
            context.SetTextColor(ConsoleTextColor.Red);
            var progressBar = context.WriteProgressBar();
            for(int i = 1; i <= 10; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                context.WriteLine(i);
                progressBar.SetValue(i * 10);
            }

            return "GO!GO!GO!";
        }
    }
}
