using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sovos.Invoicing.Application.Core.Data;
using Sovos.Invoicing.Domain.Repositories.Invoices;
using Sovos.Invoicing.Persistence.Repositories;

namespace Sovos.Invoicing.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvoicingDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<InvoicingDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<InvoicingDbContext>());

        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceQueueRepository, InvoiceQueueRepository>();

        return services;
    }
}