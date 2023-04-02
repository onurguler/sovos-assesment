using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sovos.Invoicing.BackgroundTasks.Tasks;

namespace Sovos.Invoicing.BackgroundTasks;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(x => x.UsePostgreSqlStorage(configuration.GetConnectionString("Default")));
        services.AddHangfireServer();
        services.AddScoped<ImportInvoicesJob>();
        return services;
    }

    public static IApplicationBuilder UseBackgroundTasks(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions()
        {
            Authorization = new[] { new MyAuthorizationFilter() }
        });

        RecurringJob.AddOrUpdate<ImportInvoicesJob>(nameof(ImportInvoicesJob), job => job.HandleAsync(), "*/15 * * * *");
        return app;
    }
}

public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}