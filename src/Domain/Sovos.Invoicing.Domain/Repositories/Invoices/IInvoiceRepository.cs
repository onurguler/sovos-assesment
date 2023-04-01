using Sovos.Invoicing.Domain.Entities.Invoices;
using Sovos.Invoicing.Domain.Primitives.Invoices;

namespace Sovos.Invoicing.Domain.Repositories.Invoices;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(InvoiceId id, CancellationToken cancellationToken = default);
    Task<IList<Invoice>> GetAllAsync(CancellationToken cancellationToken = default);
    void Insert(Invoice invoice);
}