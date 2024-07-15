using Hangfire;

namespace Web.Api.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        string? schedule = app.Configuration["BackgroundJobs:Outbox:Schedule"];

        IRecurringJobManager recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<Modules.Users.Infrastructure.Outbox.IProcessOutboxMessagesJob>(
            "users-outbox-processor",
            job => job.ProcessAsync(),
            schedule);

        recurringJobManager.AddOrUpdate<Modules.Posts.Infrastructure.Outbox.IProcessOutboxMessagesJob>(
            "posts-outbox-processor",
            job => job.ProcessAsync(),
            schedule);

        return app;
    }
}
