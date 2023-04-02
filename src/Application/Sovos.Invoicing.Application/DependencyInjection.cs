using System.Reflection;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Sovos.Invoicing.Application.Core.Behaviours;

namespace Sovos.Invoicing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

        return services;
    }
}
