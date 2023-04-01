using Microsoft.EntityFrameworkCore;

using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Repositories.Invoices;

namespace Sovos.Invoicing.Persistence.Repositories;

public sealed class InvoiceQueueRepository : IInvoiceQueueRepository
{
    private readonly InvoicingDbContext _dbContext;

    public InvoiceQueueRepository(InvoicingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<InvoiceQueue>> GetPendingAsync(int limit = 10, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<InvoiceQueue>()
            .Where(x => !x.Completed)
            .OrderBy(x => x.CreatedAtUtc);

        return await query.Take(limit).ToListAsync(cancellationToken);
    }

    public void Insert(InvoiceQueue invoiceQueue)
    {
        _dbContext.Set<InvoiceQueue>().Add(invoiceQueue);
    }
}