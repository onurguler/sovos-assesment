using Sovos.Invoicing.Domain.Core.Exceptions;
using Sovos.Invoicing.Domain.Primitives.Invoices;

namespace Sovos.Invoicing.Domain.Exceptions.Invoices;

public class InvoiceNotFoundException : NotFoundException
{
    public InvoiceNotFoundException(InvoiceId invoiceId) 
        : base($"Invoice does not exist with id '{invoiceId.Value}'")
    {
        Code = "Invoices.InvoiceNotFound";
    }
}