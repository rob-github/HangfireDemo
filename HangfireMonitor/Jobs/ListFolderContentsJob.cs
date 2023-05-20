using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace HangfireMonitor.Jobs
{
    [AutomaticRetry(Attempts = 2)]
    public class ListFolderContentsJob : IRegisterJob
    {
        public void RegisterJob()
        {
            RecurringJob.AddOrUpdate<ListFolderContentsJob>(
                $"{nameof(ListFolderContentsJob)}",
                job => job.Execute(@"C:\", null), Cron.Daily);
        }

        public string Execute(string path, PerformContext context)
        {
            var enumerationOptions = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = true
            };

            var numberOfFiles = 0;
            foreach (var file in Directory.GetFiles(path, "*", enumerationOptions))
            {
                numberOfFiles++;
                context.WriteLine(file);

                if (numberOfFiles > 50)
                {
                    var numberOfRetries = context.GetJobParameter<int>("RetryCount");

                    if (numberOfRetries < 2 ) throw new InvalidDataException("To many files.");

                    context.WriteLine("done ;)");

                    break;
                }
            }

            return $"{numberOfFiles} found.";
        }
    }
}
