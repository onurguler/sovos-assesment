using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sovos.Invoicing.Infrastructure.Middleware;

namespace Sovos.Invoicing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        
        return services
            .AddExceptionMiddleware()
            .AddRequestLogging()
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