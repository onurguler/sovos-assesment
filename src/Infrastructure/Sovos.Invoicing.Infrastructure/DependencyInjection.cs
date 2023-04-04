using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sovos.Invoicing.Application.Core.Abstractions.Emails;
using Sovos.Invoicing.Application.Core.Abstractions.Notifications;
using Sovos.Invoicing.Infrastructure.Emails;
using Sovos.Invoicing.Infrastructure.Middleware;
using Sovos.Invoicing.Infrastructure.Notifications;

namespace Sovos.Invoicing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

        return services
            .AddExceptionMiddleware()
            .AddRequestLogging()
            .AddTransient<IEmailService, EmailService>()
            .AddTransient<IEmailNotificationService, EmailNotificationService>()
            .AddRouting(options => options.LowercaseUrls = true);
    }


    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        return builder
            .UseStaticFiles()
            .UseExceptionMiddleware()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseRequestLogging();
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers();
        return builder;
    }
}