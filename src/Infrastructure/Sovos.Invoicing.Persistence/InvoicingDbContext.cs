using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Sovos.Invoicing.Application.Core.Data;

namespace Sovos.Invoicing.Persistence
{
    public class InvoicingDbContext : DbContext, IDbContext, IUnitOfWork
    {
        public InvoicingDbContext(DbContextOptions<InvoicingDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvoicingDbContext).Assembly);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }
    }
}