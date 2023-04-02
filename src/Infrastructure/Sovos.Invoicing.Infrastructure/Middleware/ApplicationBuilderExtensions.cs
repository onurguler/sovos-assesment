using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sovos.Invoicing.Infrastructure.Middleware;

public static class Extensions
{
    internal static IServiceCollection AddExceptionMiddleware(this IServiceCollection services)
    {
        return services.AddScoped<ExceptionMiddleware>();
    }

    internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }

    internal static IServiceCollection AddRequestLogging(this IServiceCollection services)
    {
        services.AddSingleton<RequestLoggingMiddleware>();
        services.AddScoped<ResponseLoggingMiddleware>();

        return services;
    }

    internal static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ResponseLoggingMiddleware>();

        return app;
    }
}