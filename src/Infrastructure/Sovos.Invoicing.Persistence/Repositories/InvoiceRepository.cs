using Microsoft.EntityFrameworkCore;

using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;
using Sovos.Invoicing.Domain.Repositories.Invoices;

namespace Sovos.Invoicing.Persistence.Repositories;

public sealed class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoicingDbContext _dbContext;

    public InvoiceRepository(InvoicingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Invoice>()
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<Invoice?> GetByIdAsync(InvoiceId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Invoice>()
            .AsNoTracking()
            .Where(invoice => invoice.InvoiceId == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Insert(Invoice invoice)
    {
        _dbContext.Set<Invoice>().Add(invoice);
    }
}
