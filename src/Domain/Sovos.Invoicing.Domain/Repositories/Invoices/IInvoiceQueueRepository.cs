using Sovos.Invoicing.Domain.Entities.Invoices;

namespace Sovos.Invoicing.Domain.Repositories.Invoices;

public interface IInvoiceQueueRepository
{
    void Insert(InvoiceQueue invoiceQueue);

    Task<IList<InvoiceQueue>> GetPendingAsync(int limit = 10, CancellationToken cancellationToken = default);
}
