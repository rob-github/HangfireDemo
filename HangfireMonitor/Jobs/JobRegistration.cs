namespace HangfireMonitor.Jobs
{
    public static class JobRegistration
    {
        public static IServiceCollection AddHangfireJobs<TMarker>(this IServiceCollection services)
        {
            Type registerJobType = typeof(IRegisterJob);
            var jobRegistrations = typeof(TMarker)
                .Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(registerJobType) && t.IsClass);

            foreach(var jobRegistration in jobRegistrations)
            {
                services.AddTransient(registerJobType, jobRegistration);
            }

            return services;
        }

        public static void RegisterRecurringJobs(this IServiceProvider services)
        {
            foreach (var jobRegistration in services.GetServices<IRegisterJob>())
            {
                jobRegistration.RegisterJob();
            }
        }
    }
}
